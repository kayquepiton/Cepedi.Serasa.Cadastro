using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction
{
    public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionRequest, Result<CreateTransactionResponse>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<CreateTransactionRequestHandler> _logger;

        public CreateTransactionRequestHandler(ILogger<CreateTransactionRequestHandler> logger, ITransactionRepository transactionRepository, IPersonRepository personRepository, ITransactionTypeRepository transactionTypeRepository)
        {
            _personRepository = personRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task<Result<CreateTransactionResponse>> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonAsync(request.PersonId);
            if (person is null)
            {
                return Result.Error<CreateTransactionResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalidPersonId));
            }

            var transactionType = await _transactionTypeRepository.GetTransactionTypeAsync(request.TransactionTypeId);
            if (transactionType is null)
            {
                return Result.Error<CreateTransactionResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalTransactionIdIdType));
            }

            var transaction = new TransactionEntity()
            {
                TransactionTypeId = request.TransactionTypeId,
                PersonId = request.PersonId,
                DateTime = DateTime.UtcNow,
                EstablishmentName = request.EstablishmentName,
                Value = request.Value,
            };

            await _transactionRepository.CreateTransactionAsync(transaction);

            var response = new CreateTransactionResponse(
                transaction.Id,
                transaction.TransactionTypeId,
                transaction.PersonId,
                transaction.DateTime,
                transaction.EstablishmentName,
                transaction.Value
            );

            return Result.Success(response);
        }
    }
}

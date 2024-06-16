using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction
{
    public class DeleteTransactionRequestHandler : IRequestHandler<DeleteTransactionRequest, Result<DeleteTransactionResponse>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<DeleteTransactionRequestHandler> _logger;

        public DeleteTransactionRequestHandler(ILogger<DeleteTransactionRequestHandler> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<DeleteTransactionResponse>> Handle(DeleteTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetTransactionAsync(request.Id);

            if (transaction == null)
            {
                return Result.Error<DeleteTransactionResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalidTransactionId));
            }

            await _transactionRepository.DeleteTransactionAsync(transaction.Id);

            var response = new DeleteTransactionResponse(
                transaction.Id,
                transaction.IdTransactionType,
                transaction.IdPerson,
                transaction.DateTime,
                transaction.EstablishmentName,
                transaction.Value
            );

            return Result.Success(response);
        }
    }
}

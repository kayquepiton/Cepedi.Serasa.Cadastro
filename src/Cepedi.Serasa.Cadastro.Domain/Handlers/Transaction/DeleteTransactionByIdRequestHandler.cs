using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction
{
    public class DeleteTransactionByIdRequestHandler : IRequestHandler<DeleteTransactionByIdRequest, Result<DeleteTransactionByIdResponse>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<DeleteTransactionByIdRequestHandler> _logger;

        public DeleteTransactionByIdRequestHandler(ILogger<DeleteTransactionByIdRequestHandler> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<DeleteTransactionByIdResponse>> Handle(DeleteTransactionByIdRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetTransactionAsync(request.Id);

            if (transaction is null)
            {
                return Result.Error<DeleteTransactionByIdResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalTransactionIdId));
            }

            await _transactionRepository.DeleteTransactionAsync(transaction.Id);

            var response = new DeleteTransactionByIdResponse(
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

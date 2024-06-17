using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction
{
    public class GetTransactionByIdRequestHandler : IRequestHandler<GetTransactionByIdRequest, Result<GetTransactionByIdResponse>>
    {
        private readonly ILogger<GetTransactionByIdRequestHandler> _logger;
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionByIdRequestHandler(ILogger<GetTransactionByIdRequestHandler> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<GetTransactionByIdResponse>> Handle(GetTransactionByIdRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetTransactionAsync(request.Id);

            if (transaction is null)
            {
                return Result.Error<GetTransactionByIdResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalTransactionIdId));
            }

            var response = new GetTransactionByIdResponse(
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

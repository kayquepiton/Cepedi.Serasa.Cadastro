using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction
{
    public class GetTransactionRequestHandler : IRequestHandler<GetTransactionRequest, Result<GetTransactionResponse>>
    {
        private readonly ILogger<GetTransactionRequestHandler> _logger;
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionRequestHandler(ILogger<GetTransactionRequestHandler> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<GetTransactionResponse>> Handle(GetTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetTransactionAsync(request.Id);

            if (transaction == null)
            {
                return Result.Error<GetTransactionResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalidTransactionId));
            }

            var response = new GetTransactionResponse(
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

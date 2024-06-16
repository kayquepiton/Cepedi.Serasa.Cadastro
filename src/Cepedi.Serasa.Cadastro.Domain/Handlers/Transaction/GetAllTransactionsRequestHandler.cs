using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction
{
    public class GetAllTransactionsRequestHandler : IRequestHandler<GetAllTransactionsRequest, Result<List<GetAllTransactionsResponse>>>
    {
        private readonly ILogger<GetAllTransactionsRequestHandler> _logger;
        private readonly ITransactionRepository _transactionRepository;

        public GetAllTransactionsRequestHandler(ILogger<GetAllTransactionsRequestHandler> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<List<GetAllTransactionsResponse>>> Handle(GetAllTransactionsRequest request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();

            if (transactions is null)
            {
                return Result.Error<List<GetAllTransactionsResponse>>(
                    new Shared.Exceptions.AppException(RegistrationErrors.EmptyTransactionsList));
            }

            var response = new List<GetAllTransactionsResponse>();

            foreach (var transaction in transactions)
            {
                response.Add(new GetAllTransactionsResponse(
                    transaction.Id,
                    transaction.IdTransactionType,
                    transaction.IdPerson,
                    transaction.DateTime,
                    transaction.EstablishmentName,
                    transaction.Value));
            }

            return Result.Success(response);
        }
    }
}

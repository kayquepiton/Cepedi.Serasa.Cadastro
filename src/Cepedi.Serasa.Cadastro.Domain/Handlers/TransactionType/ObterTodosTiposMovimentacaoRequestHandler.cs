using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType
{
    public class GetAllTransactionTypesRequestHandler : IRequestHandler<GetAllTransactionTypesRequest, Result<List<GetAllTransactionTypesResponse>>>
    {
        private readonly ILogger<GetAllTransactionTypesRequestHandler> _logger;
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        public GetAllTransactionTypesRequestHandler(ILogger<GetAllTransactionTypesRequestHandler> logger, ITransactionTypeRepository transactionTypeRepository)
        {
            _logger = logger;
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<Result<List<GetAllTransactionTypesResponse>>> Handle(GetAllTransactionTypesRequest request, CancellationToken cancellationToken)
        {
            var transactionTypes = await _transactionTypeRepository.GetAllTransactionTypesAsync();

            if (transactionTypes is null)
            {
                return Result.Error<List<GetAllTransactionTypesResponse>>(new Shared.Exceptions.AppException(RegistrationErrors.EmptyTransactionTypesList));
            }

            var response = new List<GetAllTransactionTypesResponse>();
            foreach (var transactionType in transactionTypes)
            {
                response.Add(new GetAllTransactionTypesResponse(transactionType.Id, transactionType.TypeName));
            }

            return Result.Success(response);
        }
    }
}

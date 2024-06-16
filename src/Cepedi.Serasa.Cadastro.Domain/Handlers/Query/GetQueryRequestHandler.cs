using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Query
{
    public class GetQueryRequestHandler :
        IRequestHandler<GetQueryRequest, Result<GetQueryResponse>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ILogger<GetQueryRequestHandler> _logger;

        public GetQueryRequestHandler(IQueryRepository queryRepository, ILogger<GetQueryRequestHandler> logger)
        {
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<Result<GetQueryResponse>> Handle(GetQueryRequest request, CancellationToken cancellationToken)
        {
            var query = await _queryRepository.GetQueryAsync(request.Id);

            if (query is null)
            {
                return Result.Error<GetQueryResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidQueryId));
            }

            var response = new GetQueryResponse(query.Id,
                                               query.IdPerson,
                                               query.Status,
                                               query.Date);

            return Result.Success(response);
        }
    }
}

using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Query
{
    public class GetAllQueriesRequestHandler : IRequestHandler<GetAllQueriesRequest, Result<List<GetAllQueriesResponse>>>
    {
        private readonly ILogger<GetAllQueriesRequestHandler> _logger;
        private readonly IQueryRepository _queryRepository;

        public GetAllQueriesRequestHandler(ILogger<GetAllQueriesRequestHandler> logger, IQueryRepository queryRepository)
        {
            _logger = logger;
            _queryRepository = queryRepository;
        }

        public async Task<Result<List<GetAllQueriesResponse>>> Handle(GetAllQueriesRequest request, CancellationToken cancellationToken)
        {
            var queries = await _queryRepository.GetAllQueriesAsync();

            if (queries is null)
            {
                return Result.Error<List<GetAllQueriesResponse>>(new Shared.Exceptions.AppException(RegistrationErrors.EmptyQueriesList));
            }

            var response = new List<GetAllQueriesResponse>();
            foreach (var query in queries)
            {
                response.Add(new GetAllQueriesResponse(query.Id,
                                                       query.PersonId,
                                                       query.Status,
                                                       query.Date));
            }

            return Result.Success(response);
        }
    }
}

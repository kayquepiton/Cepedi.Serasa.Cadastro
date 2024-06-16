using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Query
{
    public class DeleteQueryRequestHandler :
        IRequestHandler<DeleteQueryRequest, Result<DeleteQueryResponse>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ILogger<DeleteQueryRequestHandler> _logger;

        public DeleteQueryRequestHandler(IQueryRepository queryRepository, ILogger<DeleteQueryRequestHandler> logger)
        {
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<Result<DeleteQueryResponse>> Handle(DeleteQueryRequest request, CancellationToken cancellationToken)
        {
            var query = await _queryRepository.GetQueryAsync(request.Id);

            if (query is null)
            {
                return Result.Error<DeleteQueryResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidQueryId));
            }

            await _queryRepository.DeleteQueryAsync(query.Id);

            var response = new DeleteQueryResponse(query.Id,
                                                   query.IdPerson,
                                                   query.Status,
                                                   query.Date);

            return Result.Success(response);
        }
    }
}

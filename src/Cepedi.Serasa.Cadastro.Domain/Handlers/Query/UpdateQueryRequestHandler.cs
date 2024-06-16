using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Query
{
    public class UpdateQueryRequestHandler : IRequestHandler<UpdateQueryRequest, Result<UpdateQueryResponse>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ILogger<UpdateQueryRequestHandler> _logger;

        public UpdateQueryRequestHandler(IQueryRepository queryRepository, ILogger<UpdateQueryRequestHandler> logger)
        {
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<Result<UpdateQueryResponse>> Handle(UpdateQueryRequest request, CancellationToken cancellationToken)
        {
            var query = await _queryRepository.GetQueryAsync(request.Id);

            if (query is null)
            {
                return Result.Error<UpdateQueryResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidQueryId));
            }

            query.Status = request.Status;

            await _queryRepository.UpdateQueryAsync(query);
            
            var response = new UpdateQueryResponse(query.Id, 
                                                  query.IdPerson, 
                                                  query.Status, 
                                                  query.Date);

            return Result.Success(response);
        }
    }
}

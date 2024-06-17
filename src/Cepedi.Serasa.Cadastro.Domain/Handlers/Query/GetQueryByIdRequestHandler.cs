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
    public class GetQueryByIdRequestHandler : IRequestHandler<GetQueryByIdRequest, Result<GetQueryByIdResponse>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ILogger<GetQueryByIdRequestHandler> _logger;

        public GetQueryByIdRequestHandler(IQueryRepository queryRepository, ILogger<GetQueryByIdRequestHandler> logger)
        {
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<Result<GetQueryByIdResponse>> Handle(GetQueryByIdRequest request, CancellationToken cancellationToken)
        {
            var query = await _queryRepository.GetQueryAsync(request.Id);

            if (query is null)
            {
                return Result.Error<GetQueryByIdResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidQueryId));
            }

            var response = new GetQueryByIdResponse(query.Id,
                                               query.PersonId,
                                               query.Status,
                                               query.Date);

            return Result.Success(response);
        }
    }
}

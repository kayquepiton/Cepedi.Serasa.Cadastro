using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Query
{
    public class DeleteQueryByIdRequestHandler : IRequestHandler<DeleteQueryByIdRequest, Result<DeleteQueryByIdResponse>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ILogger<DeleteQueryByIdRequestHandler> _logger;

        public DeleteQueryByIdRequestHandler(IQueryRepository queryRepository, ILogger<DeleteQueryByIdRequestHandler> logger)
        {
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<Result<DeleteQueryByIdResponse>> Handle(DeleteQueryByIdRequest request, CancellationToken cancellationToken)
        {
            var query = await _queryRepository.GetQueryAsync(request.Id);

            if (query is null)
            {
                return Result.Error<DeleteQueryByIdResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidQueryId));
            }

            await _queryRepository.DeleteQueryAsync(query.Id);

            var response = new DeleteQueryByIdResponse(query.Id,
                                                   query.PersonId,
                                                   query.Status,
                                                   query.Date);

            return Result.Success(response);
        }
    }
}

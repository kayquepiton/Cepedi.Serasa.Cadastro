using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Query
{
    public class CreateQueryRequestHandler : IRequestHandler<CreateQueryRequest, Result<CreateQueryResponse>>
    {
        private readonly ILogger<CreateQueryRequestHandler> _logger;
        private readonly IQueryRepository _queryRepository;

        public CreateQueryRequestHandler(IQueryRepository queryRepository, ILogger<CreateQueryRequestHandler> logger)
        {
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<Result<CreateQueryResponse>> Handle(CreateQueryRequest request, CancellationToken cancellationToken)
        {
            var person = await _queryRepository.GetPersonForQueryAsync(request.IdPerson);
            if (person == null)
            {
                return Result.Error<CreateQueryResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidPersonId));
            }

            var query = new QueryEntity()
            {
                Status = request.Status,
                Date = request.Date,
                IdPerson = request.IdPerson,
            };

            await _queryRepository.CreateQueryAsync(query);

            var response = new CreateQueryResponse(query.Id,
                                                   query.IdPerson,
                                                   query.Status,
                                                   query.Date);
            return Result.Success(response);
        }
    }
}

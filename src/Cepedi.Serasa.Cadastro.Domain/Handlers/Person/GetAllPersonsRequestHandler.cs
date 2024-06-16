using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Person
{
    public class GetAllPersonsRequestHandler : IRequestHandler<GetAllPersonsRequest, Result<IEnumerable<GetPersonResponse>>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<GetAllPersonsRequestHandler> _logger;

        public GetAllPersonsRequestHandler(IPersonRepository personRepository, ILogger<GetAllPersonsRequestHandler> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<GetPersonResponse>>> Handle(GetAllPersonsRequest request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetPersonsAsync();

            return !persons.Any()
                ? Result.Error<IEnumerable<GetPersonResponse>>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidPersonId))
                : Result.Success(persons.Select(person => new GetPersonResponse(person.Id, person.Name, person.CPF)));
        }
    }
}

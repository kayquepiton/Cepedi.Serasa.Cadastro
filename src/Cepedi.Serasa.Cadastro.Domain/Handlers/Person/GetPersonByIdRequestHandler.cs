using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Person
{
    public class GetPersonByIdRequestHandler : IRequestHandler<GetPersonByIdRequest, Result<GetPersonResponse>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<GetPersonByIdRequestHandler> _logger;

        public GetPersonByIdRequestHandler(IPersonRepository personRepository, ILogger<GetPersonByIdRequestHandler> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<Result<GetPersonResponse>> Handle(GetPersonByIdRequest request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonAsync(request.Id);

            return person is null
                ? Result.Error<GetPersonResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidPersonId))
                : Result.Success(new GetPersonResponse(person.Id, person.Name, person.CPF));
        }
    }
}

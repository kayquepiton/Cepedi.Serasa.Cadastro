using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Person
{
    public class UpdatePersonRequestHandler
        : IRequestHandler<UpdatePersonRequest, Result<UpdatePersonResponse>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<UpdatePersonRequestHandler> _logger;

        public UpdatePersonRequestHandler(IPersonRepository personRepository, ILogger<UpdatePersonRequestHandler> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<Result<UpdatePersonResponse>> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonAsync(request.Id);

            if (person is null)
            {
                return Result.Error<UpdatePersonResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidPersonId));
            }

            person.Name = request.Name;
            person.CPF = request.CPF;
            
            await _personRepository.UpdatePersonAsync(person);

            return Result.Success(new UpdatePersonResponse(person.Id, person.Name, person.CPF));
        }
    }
}

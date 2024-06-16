using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Person
{
    public class DeletePersonByIdRequestHandler : IRequestHandler<DeletePersonByIdRequest, Result<DeletePersonByIdResponse>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<DeletePersonByIdRequestHandler> _logger;

        public DeletePersonByIdRequestHandler(ILogger<DeletePersonByIdRequestHandler> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public async Task<Result<DeletePersonByIdResponse>> Handle(DeletePersonByIdRequest request, CancellationToken cancellationToken)
        {
            var personEntity = await _personRepository.GetPersonAsync(request.Id);
            if (personEntity is null)
            {
                return Result.Error<DeletePersonByIdResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidPersonId));
            }

            await _personRepository.DeletePersonAsync(personEntity.Id);

            return Result.Success(new DeletePersonByIdResponse(personEntity.Id, personEntity.Name, personEntity.CPF));
        }
    }
}

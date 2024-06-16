using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Person;

public class CreatePersonRequestHandler
    : IRequestHandler<CreatePersonRequest, Result<CreatePersonResponse>>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<CreatePersonRequestHandler> _logger;

    public CreatePersonRequestHandler(IPersonRepository personRepository, ILogger<CreatePersonRequestHandler> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<Result<CreatePersonResponse>> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        var person = new PersonEntity
        {
            Name = request.Name,
            CPF = request.CPF
        };

        await _personRepository.CreatePersonAsync(person);

        return Result.Success(new CreatePersonResponse(person.Id, person.Name, person.CPF));
    }
}

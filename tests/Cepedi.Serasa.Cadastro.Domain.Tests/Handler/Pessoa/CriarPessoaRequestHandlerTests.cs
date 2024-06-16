using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Shared.Validators.Person;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Person;

public class CriarPersonRequestHandlerTests
{
    private readonly IPersonRepository _PersonRepository;
    private readonly ILogger<CriarPersonRequestHandler> _logger;
    private readonly CriarPersonRequestHandler _sut;

    public CriarPersonRequestHandlerTests()
    {
        _PersonRepository = Substitute.For<IPersonRepository>();
        _logger = Substitute.For<ILogger<CriarPersonRequestHandler>>();
        _sut = new CriarPersonRequestHandler(_PersonRepository, _logger);
    }

    [Fact]
    public async Task QuandoCriarPersonDeveRetornarSucesso()
    {
        //Arrange
        var PersonRequest = new CriarPersonRequest
        {
            Name = "Fernando Lima",
            CPF = "43669795006"
        };

        var Person = new PersonEntity
        {
            Name = PersonRequest.Name,
            CPF = PersonRequest.CPF
        };

        _PersonRepository.CriarPersonAsync(Arg.Is<PersonEntity>(Person => Person.Name == PersonRequest.Name
        && Person.CPF == PersonRequest.CPF)).Returns(Person);

        //Act
        var result = await _sut.Handle(PersonRequest, CancellationToken.None);

        //Assert
        result.Should().BeOfType<Result<CriarPersonResponse>>()
            .Which.Value.Name.Should().Be(PersonRequest.Name);

        result.Should().BeOfType<Result<CriarPersonResponse>>()
            .Which.Value.CPF.Should().Be(PersonRequest.CPF);

        await _PersonRepository.Received(1)
            .CriarPersonAsync(Arg.Is<PersonEntity>(Person => Person.Name == PersonRequest.Name && Person.CPF == PersonRequest.CPF));
    }

    [Fact]
    public async Task QuandoCriarPersonComDataInvalidosDeveRetornarErro()
    {
        //Arrange
        var PersonRequest = new CriarPersonRequest
        {
            Name = "Ze",
            CPF = "123"
        };

        var validator = new CriarPersonRequestValidator();

        //Act
        var validationResult = validator.Validate(PersonRequest);
        var result = await _sut.Handle(PersonRequest, CancellationToken.None);

        //Assert
        await _PersonRepository.Received(1)
            .CriarPersonAsync(Arg.Is<PersonEntity>(Person => Person.Name == PersonRequest.Name && Person.CPF == PersonRequest.CPF));

        validationResult.IsValid.Should().BeFalse();
        result.Should().BeOfType<Result<CriarPersonResponse>>();
        result.IsSuccess.Should().BeTrue();
    }
}

using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Person;
public class ObterPersonPorIdRequestHandlerTests
{
    private readonly IPersonRepository _PersonRepository;
    private readonly ILogger<ObterPersonPorIdRequestHandler> _logger;
    private readonly ObterPersonPorIdRequestHandler _sut;

    public ObterPersonPorIdRequestHandlerTests()
    {
        _PersonRepository = Substitute.For<IPersonRepository>();
        _logger = Substitute.For<ILogger<ObterPersonPorIdRequestHandler>>();
        _sut = new ObterPersonPorIdRequestHandler(_PersonRepository, _logger);
    }

    [Fact]
    public async Task QuandoBuscarIdExistenteDeveRetornarPersonResponse()
    {
        var request = new ObterPersonPorIdRequest { Id = 1 };

        var Person = new PersonEntity
        {
            Id = request.Id,
            Name = "Carlos Matos",
            CPF = "86088154004"
        };

        _PersonRepository.ObterPersonAsync(request.Id).Returns(Task.FromResult(Person));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<Result<ObterPersonResponse>>();
        result.Value.Id.Should().Be(request.Id);
        result.Value.Name.Should().Be(Person.Name);
        result.Value.CPF.Should().Be(Person.CPF);

        await _PersonRepository.Received(1).ObterPersonAsync(request.Id);
    }

    [Fact]
    public async Task QuandoBuscarPorIdInexistenteDeveRetornarNulo()
    {
        //Arrange
        var request = new ObterPersonPorIdRequest { Id = 100 };

        _PersonRepository.ObterPersonAsync(request.Id).ReturnsNull();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Value.Should().BeNull();

        await _PersonRepository.Received(1).ObterPersonAsync(request.Id);
    }
}

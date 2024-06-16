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

public class ExcluirPersonRequestHandlerTests
{
    private readonly IPersonRepository _PersonRepository;
    private readonly ILogger<ExcluirPersonPorIdRequestHandler> _logger;
    private readonly ExcluirPersonPorIdRequestHandler _sut;

    public ExcluirPersonRequestHandlerTests()
    {
        _PersonRepository = Substitute.For<IPersonRepository>();
        _logger = Substitute.For<ILogger<ExcluirPersonPorIdRequestHandler>>();
        _sut = new ExcluirPersonPorIdRequestHandler(_logger, _PersonRepository);
    }

    [Fact]
    public async Task QuandoExcluirPersonPorIdExistenteDeveRetornarSucesso()
    {
        var request = new ExcluirPersonPorIdRequest { Id = 1 };

        var Person = new PersonEntity
        {
            Id = 1,
            Name = "Carlos Matos",
            CPF = "86088154004"
        };

        _PersonRepository.ObterPersonAsync(request.Id).Returns(Task.FromResult(Person));
        _PersonRepository.ExcluirPersonAsync(request.Id).Returns(Task.FromResult(Person));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Should().BeOfType<Result<ExcluirPersonPorIdResponse>>();
        result.Value.Id.Should().Be(Person.Id);
        result.Value.Name.Should().Be(Person.Name);
        result.Value.CPF.Should().Be(Person.CPF);

        await _PersonRepository.Received(1).ObterPersonAsync(request.Id);
        await _PersonRepository.Received(1).ExcluirPersonAsync(request.Id);
    }

    [Fact]
    public async Task QuandoExcluirPersonPorIdInexistenteDeveRetornarNulo()
    {
        var request = new ExcluirPersonPorIdRequest { Id = 10 };

        _PersonRepository.ExcluirPersonAsync(request.Id).ReturnsNull();

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().BeOfType<Result<ExcluirPersonPorIdResponse>>();
        result.IsSuccess.Should().BeFalse();

        await _PersonRepository.Received(1).ObterPersonAsync(request.Id);
    }
}

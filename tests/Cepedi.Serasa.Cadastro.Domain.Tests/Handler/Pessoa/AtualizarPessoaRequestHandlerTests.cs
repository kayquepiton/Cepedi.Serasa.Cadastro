using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Shared.Validators.Person;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Person;
public class AtualizarPersonRequestHandlerTests
{
    private readonly IPersonRepository _PersonRepository;
    private readonly ILogger<AtualizarPersonRequestHandler> _logger;
    private readonly AtualizarPersonRequestHandler _sut;

    public AtualizarPersonRequestHandlerTests()
    {
        _PersonRepository = Substitute.For<IPersonRepository>();
        _logger = Substitute.For<ILogger<AtualizarPersonRequestHandler>>();
        _sut = new AtualizarPersonRequestHandler(_PersonRepository, _logger);
    }

    [Fact]
    public async Task QuandoAtualizarPersonDeveRetornarSucesso()
    {
        var request = new AtualizarPersonRequest
        {
            Id = 1,
            Name = "Carlos Matos",
            CPF = "86088154004"
        };

        var PersonDoBanco = new PersonEntity
        {
            Id = 1,
            Name = "Carlos",
            CPF = "86088154002"
        };

        var PersonAtualizada = new PersonEntity
        {
            Id = request.Id,
            Name = request.Name,
            CPF = request.CPF
        };

        _PersonRepository.ObterPersonAsync(request.Id).Returns(Task.FromResult(PersonDoBanco));
        _PersonRepository.AtualizarPersonAsync(PersonDoBanco).Returns(Task.FromResult(PersonAtualizada));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().BeOfType<Result<AtualizarPersonResponse>>().Which.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(request.Id);
        result.Value.Name.Should().Be(request.Name);
        result.Value.CPF.Should().Be(request.CPF);

        await _PersonRepository.Received(1).ObterPersonAsync(request.Id);
        await _PersonRepository.Received(1).AtualizarPersonAsync(PersonDoBanco);
    }

    [Fact]
    public async Task QuandoTentarAtualizarPersonComIdQueNaoExisteDeveRetornarErro()
    {
        var request = new AtualizarPersonRequest
        {
            Id = 50,
            Name = "Zé",
            CPF = "1234"
        };

        _PersonRepository
            .ObterPersonAsync(request.Id).ReturnsNull();

        var result = await _sut.Handle(request, CancellationToken.None);

        await _PersonRepository.Received(1).ObterPersonAsync(request.Id);

        result.Should().BeOfType<Result<AtualizarPersonResponse>>();
        result.IsSuccess.Should().BeFalse();
    }
}

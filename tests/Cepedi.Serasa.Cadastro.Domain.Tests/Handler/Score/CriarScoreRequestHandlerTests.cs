using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Score;

public class CriarScoreRequestHandlerTests
{
    private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
    private readonly IPersonRepository _PersonRepository = Substitute.For<IPersonRepository>();
    private readonly ILogger<CriarScoreRequestHandler> _logger = Substitute.For<ILogger<CriarScoreRequestHandler>>();
    private readonly CriarScoreRequestHandler _sut;

    public CriarScoreRequestHandlerTests()
    {
        _sut = new CriarScoreRequestHandler(_scoreRepository, _logger, _PersonRepository);
    }

    [Fact]
    public async Task Handle_QuandoCriarScore_DeveRetornarSucesso()
    {
        // Arrange
        var Person = new PersonEntity
        {
            Id = 1,
            Name = "João",
            CPF = "12345678901"
        };

        var request = new CriarScoreRequest
        {
            IdPerson = Person.Id,
            Score = 750
        };

        _PersonRepository.ObterPersonAsync(request.IdPerson).Returns(Task.FromResult(Person));
        _scoreRepository.ObterPersonScoreAsync(request.IdPerson).Returns(Task.FromResult<ScoreEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarScoreResponse>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.IdPerson.Should().Be(request.IdPerson);
        result.Value.Score.Should().Be(request.Score);

        await _PersonRepository.Received(1).ObterPersonAsync(request.IdPerson);
        await _scoreRepository.Received(1).ObterPersonScoreAsync(request.IdPerson);
        await _scoreRepository.Received(1).CriarScoreAsync(Arg.Is<ScoreEntity>(s => s.IdPerson == request.IdPerson && s.Score == request.Score));
    }

    [Fact]
    public async Task Handle_QuandoPersonNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var PersonId = 999; // ID inválido que não existe no repositório
        var request = new CriarScoreRequest
        {
            IdPerson = PersonId,
            Score = 750
        };

        _PersonRepository.ObterPersonAsync(request.IdPerson).Returns(Task.FromResult<PersonEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarScoreResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
            .Which.ErrorResult.Should().Be(RegistrationErrors.IdPersonInvalido);

        await _PersonRepository.Received(1).ObterPersonAsync(request.IdPerson);
        await _scoreRepository.DidNotReceive().ObterPersonScoreAsync(Arg.Any<int>());
        await _scoreRepository.DidNotReceiveWithAnyArgs().CriarScoreAsync(Arg.Any<ScoreEntity>());
    }

    [Fact]
    public async Task Handle_QuandoScoreJaExistir_DeveRetornarErro()
    {
        // Arrange
        var Person = new PersonEntity
        {
            Id = 1,
            Name = "João",
            CPF = "12345678901"
        };

        var scoreEntity = new ScoreEntity
        {
            Id = 1,
            IdPerson = Person.Id,
            Score = 800
        };

        var request = new CriarScoreRequest
        {
            IdPerson = Person.Id,
            Score = 750
        };

        _PersonRepository.ObterPersonAsync(request.IdPerson).Returns(Task.FromResult(Person));
        _scoreRepository.ObterPersonScoreAsync(request.IdPerson).Returns(Task.FromResult(scoreEntity));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarScoreResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
            .Which.ErrorResult.Should().Be(RegistrationErrors.ScoreJaExistente);

        await _PersonRepository.Received(1).ObterPersonAsync(request.IdPerson);
        await _scoreRepository.Received(1).ObterPersonScoreAsync(request.IdPerson);
        await _scoreRepository.DidNotReceiveWithAnyArgs().CriarScoreAsync(Arg.Any<ScoreEntity>());
    }
}


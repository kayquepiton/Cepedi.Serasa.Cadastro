using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Consulta;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Consulta;
public class CriarConsultaRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly IPersonRepository _PersonRepository = Substitute.For<IPersonRepository>();
    private readonly ILogger<CriarConsultaRequestHandler> _logger = Substitute.For<ILogger<CriarConsultaRequestHandler>>();
    private readonly CriarConsultaRequestHandler _sut;

    public CriarConsultaRequestHandlerTests()
    {
        _sut = new CriarConsultaRequestHandler(_consultaRepository, _PersonRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoCriarConsulta_DeveRetornarSucesso()
    {
        // Arrange
        var Person = new PersonEntity
        {
            Id = 1,
            Name = "Pedro",
            CPF = "98765432110"
        };

        var request = new CriarConsultaRequest
        {
            Status = true,
            Data = DateTime.Now,
            IdPerson = Person.Id
        };

        var consultaCriada = new QueryEntity
        {
            Id = 1,
            Status = request.Status,
            Data = request.Data,
            IdPerson = Person.Id
        };

        _consultaRepository.ObterPersonConsultaAsync(request.IdPerson).Returns(Task.FromResult(Person));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarConsultaResponse>>()
                    .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.IdPerson.Should().Be(request.IdPerson);
        result.Value.Status.Should().Be(request.Status);
        result.Value.Data.Should().Be(request.Data);

        await _consultaRepository.Received(1).ObterPersonConsultaAsync(request.IdPerson);
        await _consultaRepository.Received(1).CriarConsultaAsync(Arg.Any<QueryEntity>());
    }

    [Fact]
    public async Task Handle_QuandoPersonNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var PersonId = 999; // ID inválido que não existe no repositório
        var request = new CriarConsultaRequest
        {
            Status = true,
            Data = DateTime.Now,
            IdPerson = PersonId
        };

        _consultaRepository.ObterPersonConsultaAsync(request.IdPerson).Returns(Task.FromResult<PersonEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarConsultaResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<AppException>()
            .Which.ErrorResult.Should().Be(RegistrationErrors.IdPersonInvalido);

        await _consultaRepository.Received(1).ObterPersonConsultaAsync(request.IdPerson);
        await _consultaRepository.DidNotReceive().CriarConsultaAsync(Arg.Any<QueryEntity>());
    }
}


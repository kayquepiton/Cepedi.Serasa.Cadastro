using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Movimentacao;
public class CriarMovimentacaoRequestHandlerTests
{
    private readonly IMovimentacaoRepository _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
    private readonly IPersonRepository _PersonRepository = Substitute.For<IPersonRepository>();
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<CriarMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<CriarMovimentacaoRequestHandler>>();
    private readonly CriarMovimentacaoRequestHandler _sut;

    public CriarMovimentacaoRequestHandlerTests()
    {
        _sut = new CriarMovimentacaoRequestHandler(_logger, _movimentacaoRepository, _PersonRepository, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoCriarMovimentacao_DeveRetornarSucesso()
    {
        // Arrange
        var tipoMovimentacao = new TransactionTypeEntity
        {
            Id = 1,
            TypeName = "Compra"
        };

        var Person = new PersonEntity
        {
            Id = 1,
            Name = "João",
            CPF = "12345678901"
        };

        var request = new CriarMovimentacaoRequest
        {
            IdTipoMovimentacao = tipoMovimentacao.Id,
            IdPerson = Person.Id,
            NameEstabelecimento = "Exemplo Loja",
            Valor = 100.0m
        };

        var movimentacaoCriada = new TransactionEntity
        {
            Id = 1,
            IdTipoMovimentacao = tipoMovimentacao.Id,
            IdPerson = Person.Id,
            DataHora = DateTime.UtcNow,
            NameEstabelecimento = request.NameEstabelecimento,
            Valor = request.Valor,
        };

        _PersonRepository.ObterPersonAsync(request.IdPerson).Returns(Task.FromResult(Person));
        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult(tipoMovimentacao));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.IdTipoMovimentacao.Should().Be(tipoMovimentacao.Id);
        result.Value.IdPerson.Should().Be(Person.Id);
        result.Value.NameEstabelecimento.Should().Be(request.NameEstabelecimento);
        result.Value.Valor.Should().Be(request.Valor);

        await _PersonRepository.Received(1).ObterPersonAsync(request.IdPerson);
        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
        await _movimentacaoRepository.Received(1).CriarMovimentacaoAsync(Arg.Any<TransactionEntity>());
    }

    [Fact]
    public async Task Handle_QuandoTipoMovimentacaoNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var tipoMovimentacaoId = 999; // ID inválido que não existe no repositório
        var Person = new PersonEntity
        {
            Id = 1,
            Name = "João",
            CPF = "12345678901"
        };

        var request = new CriarMovimentacaoRequest
        {
            IdTipoMovimentacao = tipoMovimentacaoId,
            IdPerson = Person.Id,
            NameEstabelecimento = "Exemplo Loja",
            Valor = 100.0m
        };

        _PersonRepository.ObterPersonAsync(request.IdPerson).Returns(Task.FromResult(Person));
        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult<TransactionTypeEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarMovimentacaoResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.IdTipoMovimentacaoInvalido);

        await _PersonRepository.Received(1).ObterPersonAsync(request.IdPerson);
        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
        await _movimentacaoRepository.DidNotReceiveWithAnyArgs().CriarMovimentacaoAsync(Arg.Any<TransactionEntity>());                }

    [Fact]
    public async Task Handle_QuandoPersonNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var tipoMovimentacao = new TransactionTypeEntity
        {
            Id = 1,
            TypeName = "Compra"
        };

        var PersonId = 999; // ID inválido que não existe no repositório
        var request = new CriarMovimentacaoRequest
        {
            IdTipoMovimentacao = tipoMovimentacao.Id,
            IdPerson = PersonId,
            NameEstabelecimento = "Exemplo Loja",
            Valor = 100.0m
        };

        _PersonRepository.ObterPersonAsync(request.IdPerson).Returns(Task.FromResult<PersonEntity>(null));
        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult<TransactionTypeEntity>(tipoMovimentacao));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarMovimentacaoResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
            .Which.ErrorResult.Should().Be(RegistrationErrors.IdPersonInvalido);

        await _PersonRepository.Received(1).ObterPersonAsync(request.IdPerson);
        await _tipoMovimentacaoRepository.DidNotReceive().ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
        await _movimentacaoRepository.DidNotReceiveWithAnyArgs().CriarMovimentacaoAsync(Arg.Any<TransactionEntity>());
    }

}


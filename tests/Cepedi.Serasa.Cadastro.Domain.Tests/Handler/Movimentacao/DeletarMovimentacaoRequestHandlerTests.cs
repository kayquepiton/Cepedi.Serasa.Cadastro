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
public class DeletarMovimentacaoRequestHandlerTests
{
    private readonly IMovimentacaoRepository _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
    private readonly ILogger<DeletarMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<DeletarMovimentacaoRequestHandler>>();
    private readonly DeletarMovimentacaoRequestHandler _sut;

    public DeletarMovimentacaoRequestHandlerTests()
    {
        _sut = new DeletarMovimentacaoRequestHandler(_logger, _movimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoDeletarMovimentacao_DeveRetornarSucesso()
    {
        // Arrange
        var idMovimentacao = 1;

        var movimentacaoExistente = new TransactionEntity
        {
            Id = idMovimentacao,
            IdTipoMovimentacao = 1,
            IdPerson = 1,
            DataHora = DateTime.UtcNow.AddDays(-1),
            NameEstabelecimento = "Exemplo Loja",
            Valor = 100.0m
        };

        _movimentacaoRepository.ObterMovimentacaoAsync(idMovimentacao)
                                .Returns(Task.FromResult(movimentacaoExistente));

        // Act
        var result = await _sut.Handle(new DeletarMovimentacaoRequest { Id = idMovimentacao }, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        // Verificar se o método no repositório foi chamado corretamente
        await _movimentacaoRepository.Received(1).ObterMovimentacaoAsync(idMovimentacao);
        await _movimentacaoRepository.Received(1).DeletarMovimentacaoAsync(idMovimentacao);
    }

    [Fact]
    public async Task Handle_QuandoDeletarMovimentacaoInexistente_DeveRetornarFalha()
    {
        // Arrange
        var idMovimentacaoInexistente = 99;

        _movimentacaoRepository.ObterMovimentacaoAsync(idMovimentacaoInexistente)
                                .Returns(Task.FromResult<TransactionEntity>(null));

        // Act
        var result = await _sut.Handle(new DeletarMovimentacaoRequest { Id = idMovimentacaoInexistente }, CancellationToken.None);

        // Assert
        result.Should().NotBeNull(); // Verifica se o resultado não é nulo
        result.IsSuccess.Should().BeFalse(); // Verifica se a operação falhou
    }
}


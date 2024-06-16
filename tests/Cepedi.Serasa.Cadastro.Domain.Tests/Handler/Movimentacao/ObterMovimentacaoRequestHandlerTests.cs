using Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Movimentacao;
public class ObterMovimentacaoRequestHandlerTests
{
    private readonly IMovimentacaoRepository _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
    private readonly ILogger<ObterMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<ObterMovimentacaoRequestHandler>>();
    private readonly ObterMovimentacaoRequestHandler _sut;

    public ObterMovimentacaoRequestHandlerTests()
    {
        _sut = new ObterMovimentacaoRequestHandler(_logger, _movimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoObterMovimentacaoExistente_DeveRetornarMovimentacao()
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

        var request = new ObterMovimentacaoRequest { Id = idMovimentacao };

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        result.Value.Id.Should().Be(movimentacaoExistente.Id);
        result.Value.IdTipoMovimentacao.Should().Be(movimentacaoExistente.IdTipoMovimentacao);
        result.Value.IdPerson.Should().Be(movimentacaoExistente.IdPerson);
        result.Value.DataHora.Should().BeCloseTo(movimentacaoExistente.DataHora, precision: TimeSpan.FromSeconds(1));
        result.Value.NameEstabelecimento.Should().Be(movimentacaoExistente.NameEstabelecimento);
        result.Value.Valor.Should().Be(movimentacaoExistente.Valor);

        // Verifica se o método no repositório foi chamado corretamente
        await _movimentacaoRepository.Received(1).ObterMovimentacaoAsync(idMovimentacao);
    }

    [Fact]
    public async Task Handle_QuandoObterMovimentacaoInexistente_DeveRetornarNulo()
    {
        // Arrange
        var idMovimentacaoInexistente = 99;

        _movimentacaoRepository.ObterMovimentacaoAsync(idMovimentacaoInexistente)
                                .Returns(Task.FromResult<TransactionEntity>(null));

        var request = new ObterMovimentacaoRequest { Id = idMovimentacaoInexistente };

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeNull();

        // Verifica se o método no repositório foi chamado corretamente
        await _movimentacaoRepository.Received(1).ObterMovimentacaoAsync(idMovimentacaoInexistente);
    }
}

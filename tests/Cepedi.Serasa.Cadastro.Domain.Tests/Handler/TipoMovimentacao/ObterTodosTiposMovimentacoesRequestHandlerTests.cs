using Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.TipoMovimentacao;

public class ObterTodosTiposMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<ObterTodosTiposMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<ObterTodosTiposMovimentacaoRequestHandler>>();
    private readonly ObterTodosTiposMovimentacaoRequestHandler _sut;

    public ObterTodosTiposMovimentacaoRequestHandlerTests()
    {
        _sut = new ObterTodosTiposMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoObterTodosTiposMovimentacao_DeveRetornarListaTiposMovimentacao()
    {
        // Arrange
        var tiposMovimentacao = new List<TransactionTypeEntity>
        {
            new TransactionTypeEntity { Id = 1, TypeName = "Compra" },
            new TransactionTypeEntity { Id = 2, TypeName = "Venda" }
        };

        _tipoMovimentacaoRepository.ObterTodosTiposMovimentacaoAsync()
                                    .Returns(Task.FromResult(tiposMovimentacao));

        var request = new ObterTodosTiposMovimentacaoRequest();

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        // Verifica se as listas têm o mesmo número de elementos
        result.Value.Should().HaveCount(tiposMovimentacao.Count);

        // Verifica se cada elemento na lista resultante corresponde ao elemento correspondente na lista original
        for (int i = 0; i < tiposMovimentacao.Count; i++)
        {
            result.Value[i].Id.Should().Be(tiposMovimentacao[i].Id);
            result.Value[i].TypeName.Should().Be(tiposMovimentacao[i].TypeName);
        }

        // Verifica se o método no repositório foi chamado corretamente
        await _tipoMovimentacaoRepository.Received(1).ObterTodosTiposMovimentacaoAsync();
    }

    [Fact]
    public async Task Handle_QuandoNaoExistemTiposMovimentacao_DeveRetornarListaVazia()
    {
        // Arrange
        var tiposMovimentacaoVazias = new List<TransactionTypeEntity>();

        _tipoMovimentacaoRepository.ObterTodosTiposMovimentacaoAsync()
                                    .Returns(Task.FromResult(tiposMovimentacaoVazias));

        var request = new ObterTodosTiposMovimentacaoRequest();

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty(); // Verifica se a lista de tipos de movimentação está vazia

        // Verifica se o método no repositório foi chamado corretamente
        await _tipoMovimentacaoRepository.Received(1).ObterTodosTiposMovimentacaoAsync();
    }

}

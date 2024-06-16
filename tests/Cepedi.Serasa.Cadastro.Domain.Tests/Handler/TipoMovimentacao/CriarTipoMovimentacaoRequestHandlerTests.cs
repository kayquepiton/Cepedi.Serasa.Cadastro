using Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.TipoMovimentacao;

public class CriarTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<CriarTipoMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<CriarTipoMovimentacaoRequestHandler>>();
    private readonly CriarTipoMovimentacaoRequestHandler _sut;

    public CriarTipoMovimentacaoRequestHandlerTests()
    {
        _sut = new CriarTipoMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoCriarTipoMovimentacao_DeveRetornarSucesso()
    {
        // Arrange
        var request = new CriarTipoMovimentacaoRequest
        {
            TypeName = "Venda"
        };

        var tipoMovimentacao = new TransactionTypeEntity
        {
            Id = 1,
            TypeName = request.TypeName
        };

        _tipoMovimentacaoRepository
            .When(repo => repo.CriarTipoMovimentacaoAsync(Arg.Any<TransactionTypeEntity>()))
            .Do(callInfo => 
            {
                var tipoMov = callInfo.Arg<TransactionTypeEntity>();
                tipoMov.Id = tipoMovimentacao.Id;
            });

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarTipoMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(tipoMovimentacao.Id);
        result.Value.TypeName.Should().Be(request.TypeName);

        await _tipoMovimentacaoRepository.Received(1).CriarTipoMovimentacaoAsync(Arg.Is<TransactionTypeEntity>(
            tm => tm.TypeName == request.TypeName
        ));
    }
}

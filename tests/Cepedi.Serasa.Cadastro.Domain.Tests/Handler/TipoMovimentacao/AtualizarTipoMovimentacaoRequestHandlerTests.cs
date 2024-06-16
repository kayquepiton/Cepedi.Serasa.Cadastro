using Cepedi.Serasa.Cadastro.Shared.Enums;
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

public class AtualizarTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<AtualizarTipoMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<AtualizarTipoMovimentacaoRequestHandler>>();
    private readonly AtualizarTipoMovimentacaoRequestHandler _sut;

    public AtualizarTipoMovimentacaoRequestHandlerTests()
    {
        _sut = new AtualizarTipoMovimentacaoRequestHandler(_tipoMovimentacaoRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoAtualizarTipoMovimentacao_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarTipoMovimentacaoRequest
        {
            Id = 1,
            TypeName = "Nova Venda"
        };

        var tipoMovimentacaoExistente = new TransactionTypeEntity
        {
            Id = request.Id,
            TypeName = "Venda"
        };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id).Returns(Task.FromResult(tipoMovimentacaoExistente));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarTipoMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(tipoMovimentacaoExistente.Id);
        result.Value.TypeName.Should().Be(request.TypeName);

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.Id);
        await _tipoMovimentacaoRepository.Received(1).AtualizarTipoMovimentacaoAsync(Arg.Is<TransactionTypeEntity>(
            tm => tm.Id == request.Id &&
                  tm.TypeName == request.TypeName
        ));
    }

    [Fact]
    public async Task Handle_QuandoTipoMovimentacaoNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarTipoMovimentacaoRequest
        {
            Id = 1,
            TypeName = "Nova Venda"
        };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id).Returns(Task.FromResult<TransactionTypeEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarTipoMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.IdTipoMovimentacaoInvalido);
    }
}

using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Transaction;
public class UpdateTransactionRequestHandlerTests
{
    private readonly ITransactionRepository _TransactionRepository = Substitute.For<ITransactionRepository>();
    private readonly ITransactionTypeRepository _TransactionTypeRepository = Substitute.For<ITransactionTypeRepository>();
    private readonly ILogger<UpdateTransactionRequestHandler> _logger = Substitute.For<ILogger<UpdateTransactionRequestHandler>>();
    private readonly UpdateTransactionRequestHandler _sut;

    public UpdateTransactionRequestHandlerTests()
    {
        _sut = new UpdateTransactionRequestHandler(_TransactionTypeRepository, _TransactionRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoUpdateTransaction_DeveRetornarSucesso()
    {
        // Arrange
        var request = new UpdateTransactionRequest
        {
            Id = 1,
            TransactionTypeId = 2,
            DateTime = DateTime.Parse("2024-05-17T16:58:27.845Z"),
            EstablishmentName = "Nova Loja",
            Value = 200.0m
        };

        var TransactionExistente = new TransactionEntity
        {
            Id = request.Id,
            TransactionTypeId = 1,
            PersonId = 1,
            DateTime = DateTime.UtcNow.AddDays(-1),
            EstablishmentName = "Exemplo Loja",
            Value = 100.0m
        };

        var TransactionType = new TransactionTypeEntity
        {
            Id = request.TransactionTypeId,
            TypeName = "Venda"
        };

        _TransactionRepository.GetTransactionAsync(request.Id).Returns(Task.FromResult(TransactionExistente));
        _TransactionTypeRepository.GetTransactionTypeAsync(request.TransactionTypeId).Returns(Task.FromResult(TransactionType));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<UpdateTransactionResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(TransactionExistente.Id);
        result.Value.TransactionTypeId.Should().Be(TransactionExistente.TransactionTypeId);
        result.Value.PersonId.Should().Be(TransactionExistente.PersonId);
        result.Value.EstablishmentName.Should().Be(TransactionExistente.EstablishmentName);
        result.Value.Value.Should().Be(TransactionExistente.Value);

        await _TransactionRepository.Received(1).GetTransactionAsync(request.Id);
        await _TransactionTypeRepository.Received(1).GetTransactionTypeAsync(request.TransactionTypeId);
        await _TransactionRepository.Received(1).UpdateTransactionAsync(Arg.Is<TransactionEntity>(
            m => m.Id == request.Id &&
                    m.TransactionTypeId == request.TransactionTypeId &&
                    m.DateTime == request.DateTime &&
                    m.EstablishmentName == request.EstablishmentName &&
                    m.Value == request.Value
        ));
    }

    [Fact]
    public async Task Handle_QuandoTransactionTypeNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var request = new UpdateTransactionRequest
        {
            Id = 1,
            TransactionTypeId = 2,
            DateTime = DateTime.Parse("2024-05-17T16:58:27.845Z"),
            EstablishmentName = "Nova Loja",
            Value = 200.0m
        };

        _TransactionTypeRepository.GetTransactionTypeAsync(request.TransactionTypeId).Returns(Task.FromResult<TransactionTypeEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<UpdateTransactionResponse>>()
                .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.InvalTransactionIdId);
    }

    [Fact]
    public async Task Handle_QuandoTransactionNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var request = new UpdateTransactionRequest
        {
            Id = 1,
            TransactionTypeId = 2,
            DateTime = DateTime.Parse("2024-05-17T16:58:27.845Z"),
            EstablishmentName = "Nova Loja",
            Value = 200.0m
        };

        var TransactionType = new TransactionTypeEntity
        {
            Id = request.TransactionTypeId,
            TypeName = "Venda"
        };

        _TransactionTypeRepository.GetTransactionTypeAsync(request.TransactionTypeId).Returns(Task.FromResult(TransactionType));
        _TransactionRepository.GetTransactionAsync(request.Id).Returns(Task.FromResult<TransactionEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<UpdateTransactionResponse>>()
                .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.InvalTransactionIdId);
    }
}


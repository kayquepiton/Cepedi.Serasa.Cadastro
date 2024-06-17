using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Transaction
{
    public class DeleteTransactionByIdRequestHandlerTests
    {
        private readonly ITransactionRepository _transactionRepository = Substitute.For<ITransactionRepository>();
        private readonly ILogger<DeleteTransactionByIdRequestHandler> _logger = Substitute.For<ILogger<DeleteTransactionByIdRequestHandler>>();
        private readonly DeleteTransactionByIdRequestHandler _sut;

        public DeleteTransactionByIdRequestHandlerTests()
        {
            _sut = new DeleteTransactionByIdRequestHandler(_logger, _transactionRepository);
        }

        [Fact]
        public async Task Handle_WhenDeleteTransaction_ShouldReturnSuccess()
        {
            // Arrange
            var transactionId = 1;

            var existingTransaction = new TransactionEntity
            {
                Id = transactionId,
                TransactionTypeId = 1,
                PersonId = 1,
                DateTime = DateTime.UtcNow.AddDays(-1),
                EstablishmentName = "Example Store",
                Value = 100.0m
            };

            _transactionRepository.GetTransactionAsync(transactionId)
                                  .Returns(Task.FromResult(existingTransaction));

            // Act
            var result = await _sut.Handle(new DeleteTransactionByIdRequest { Id = transactionId }, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<DeleteTransactionByIdResponse>>()
                  .Which.IsSuccess.Should().BeTrue();

            // Verify if the method on the repository was called correctly
            await _transactionRepository.Received(1).GetTransactionAsync(transactionId);
            await _transactionRepository.Received(1).DeleteTransactionAsync(transactionId);
        }

        [Fact]
        public async Task Handle_WhenDeleteNonExistingTransaction_ShouldReturnFailure()
        {
            // Arrange
            var nonExistingTransactionId = 99;

            _transactionRepository.GetTransactionAsync(nonExistingTransactionId)
                                  .Returns(Task.FromResult<TransactionEntity>(null));

            // Act
            var result = await _sut.Handle(new DeleteTransactionByIdRequest { Id = nonExistingTransactionId }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull(); // Verify if the result is not null
            result.IsSuccess.Should().BeFalse(); // Verify if the operation failed
        }
    }
}

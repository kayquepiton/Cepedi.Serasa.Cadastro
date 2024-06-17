using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Transaction
{
    public class GetTransactionRequestHandlerTests
    {
        private readonly ITransactionRepository _transactionRepository = Substitute.For<ITransactionRepository>();
        private readonly ILogger<GetTransactionByIdRequestHandler> _logger = Substitute.For<ILogger<GetTransactionByIdRequestHandler>>();
        private readonly GetTransactionByIdRequestHandler _sut;

        public GetTransactionRequestHandlerTests()
        {
            _sut = new GetTransactionByIdRequestHandler(_logger, _transactionRepository);
        }

        [Fact]
        public async Task Handle_WhenGetExistingTransaction_ShouldReturnTransaction()
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

            var request = new GetTransactionByIdRequest { Id = transactionId };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            result.Value.Id.Should().Be(existingTransaction.Id);
            result.Value.TransactionTypeId.Should().Be(existingTransaction.TransactionTypeId);
            result.Value.PersonId.Should().Be(existingTransaction.PersonId);
            result.Value.DateTime.Should().BeCloseTo(existingTransaction.DateTime, precision: TimeSpan.FromSeconds(1));
            result.Value.EstablishmentName.Should().Be(existingTransaction.EstablishmentName);
            result.Value.Value.Should().Be(existingTransaction.Value);

            // Verify that the repository method was called correctly
            await _transactionRepository.Received(1).GetTransactionAsync(transactionId);
        }

        [Fact]
        public async Task Handle_WhenGetNonExistingTransaction_ShouldReturnNull()
        {
            // Arrange
            var nonExistingTransactionId = 99;

            _transactionRepository.GetTransactionAsync(nonExistingTransactionId)
                                   .Returns(Task.FromResult<TransactionEntity>(null));

            var request = new GetTransactionByIdRequest { Id = nonExistingTransactionId };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();

            // Verify that the repository method was called correctly
            await _transactionRepository.Received(1).GetTransactionAsync(nonExistingTransactionId);
        }
    }
}

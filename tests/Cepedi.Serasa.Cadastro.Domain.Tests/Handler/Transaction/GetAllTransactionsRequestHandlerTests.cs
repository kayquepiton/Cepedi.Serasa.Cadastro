using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Transaction
{
    public class GetAllTransactionsRequestHandlerTests
    {
        private readonly ITransactionRepository _transactionRepository = Substitute.For<ITransactionRepository>();
        private readonly ILogger<GetAllTransactionsRequestHandler> _logger = Substitute.For<ILogger<GetAllTransactionsRequestHandler>>();
        private readonly GetAllTransactionsRequestHandler _sut;

        public GetAllTransactionsRequestHandlerTests()
        {
            _sut = new GetAllTransactionsRequestHandler(_logger, _transactionRepository);
        }

        [Fact]
        public async Task Handle_WhenGetAllTransactions_ShouldReturnListOfTransactions()
        {
            // Arrange
            var transactions = new List<TransactionEntity>
            {
                new TransactionEntity { Id = 1, TransactionTypeId = 1, PersonId = 1, DateTime = DateTime.UtcNow.AddDays(-1), EstablishmentName = "Example Store 1", Value = 100.0m },
                new TransactionEntity { Id = 2, TransactionTypeId = 2, PersonId = 2, DateTime = DateTime.UtcNow.AddDays(-2), EstablishmentName = "Example Store 2", Value = 200.0m }
            };

            _transactionRepository.GetAllTransactionsAsync()
                                .Returns(Task.FromResult(transactions));

            var request = new GetAllTransactionsRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            // Verify if the lists have the same number of elements
            result.Value.Should().HaveCount(transactions.Count);

            // Verify if each element in the resulting list corresponds to the corresponding element in the original list
            for (int i = 0; i < transactions.Count; i++)
            {
                result.Value[i].Id.Should().Be(transactions[i].Id);
                result.Value[i].TransactionTypeId.Should().Be(transactions[i].TransactionTypeId);
                result.Value[i].PersonId.Should().Be(transactions[i].PersonId);
                result.Value[i].DateTime.Should().Be(transactions[i].DateTime);
                result.Value[i].EstablishmentName.Should().Be(transactions[i].EstablishmentName);
                result.Value[i].Value.Should().Be(transactions[i].Value);
            }

            // Verify if the method on the repository was called correctly
            await _transactionRepository.Received(1).GetAllTransactionsAsync();
        }

        [Fact]
        public async Task Handle_WhenNoTransactionsExist_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyTransactions = new List<TransactionEntity>();

            _transactionRepository.GetAllTransactionsAsync()
                                .Returns(Task.FromResult(emptyTransactions));

            var request = new GetAllTransactionsRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEmpty(); // Verify if the list of transactions is empty

            // Verify if the method on the repository was called correctly
            await _transactionRepository.Received(1).GetAllTransactionsAsync();
        }
    }
}

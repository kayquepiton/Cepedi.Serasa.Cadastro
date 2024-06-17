using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.TransactionType
{
    public class GetAllTransactionTypesRequestHandlerTests
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository = Substitute.For<ITransactionTypeRepository>();
        private readonly ILogger<GetAllTransactionTypesRequestHandler> _logger = Substitute.For<ILogger<GetAllTransactionTypesRequestHandler>>();
        private readonly GetAllTransactionTypesRequestHandler _sut;

        public GetAllTransactionTypesRequestHandlerTests()
        {
            _sut = new GetAllTransactionTypesRequestHandler(_logger, _transactionTypeRepository);
        }

        [Fact]
        public async Task Handle_WhenGetAllTransactionTypes_ShouldReturnListOfTransactionTypes()
        {
            // Arrange
            var transactionTypes = new List<TransactionTypeEntity>
            {
                new TransactionTypeEntity { Id = 1, TypeName = "Purchase" },
                new TransactionTypeEntity { Id = 2, TypeName = "Sale" }
            };

            _transactionTypeRepository.GetAllTransactionTypesAsync()
                                      .Returns(Task.FromResult(transactionTypes));

            var request = new GetAllTransactionTypesRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            // Check if both lists have the same number of elements
            result.Value.Should().HaveCount(transactionTypes.Count);

            // Check if each element in the resulting list corresponds to the corresponding element in the original list
            for (int i = 0; i < transactionTypes.Count; i++)
            {
                result.Value[i].Id.Should().Be(transactionTypes[i].Id);
                result.Value[i].TypeName.Should().Be(transactionTypes[i].TypeName);
            }

            // Verify if the method on the repository was called correctly
            await _transactionTypeRepository.Received(1).GetAllTransactionTypesAsync();
        }

        [Fact]
        public async Task Handle_WhenNoTransactionTypesExist_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyTransactionTypes = new List<TransactionTypeEntity>();

            _transactionTypeRepository.GetAllTransactionTypesAsync()
                                      .Returns(Task.FromResult(emptyTransactionTypes));

            var request = new GetAllTransactionTypesRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEmpty(); // Check if the list of transaction types is empty

            // Verify if the method on the repository was called correctly
            await _transactionTypeRepository.Received(1).GetAllTransactionTypesAsync();
        }
    }
}

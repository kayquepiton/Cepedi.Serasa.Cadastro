using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.TransactionType
{
    public class CreateTransactionTypeRequestHandlerTests
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository = Substitute.For<ITransactionTypeRepository>();
        private readonly ILogger<CreateTransactionTypeRequestHandler> _logger = Substitute.For<ILogger<CreateTransactionTypeRequestHandler>>();
        private readonly CreateTransactionTypeRequestHandler _sut;

        public CreateTransactionTypeRequestHandlerTests()
        {
            _sut = new CreateTransactionTypeRequestHandler(_logger, _transactionTypeRepository);
        }

        [Fact]
        public async Task Handle_WhenCreateTransactionType_ShouldReturnSuccess()
        {
            // Arrange
            var request = new CreateTransactionTypeRequest
            {
                TypeName = "Sale"
            };

            var transactionType = new TransactionTypeEntity
            {
                Id = 1,
                TypeName = request.TypeName
            };

            _transactionTypeRepository
                .CreateTransactionTypeAsync(Arg.Any<TransactionTypeEntity>())
                .Returns(Task.FromResult(transactionType));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<CreateTransactionTypeResponse>>()
                .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(transactionType.Id);
            result.Value.TypeName.Should().Be(request.TypeName);

            await _transactionTypeRepository.Received(1).CreateTransactionTypeAsync(Arg.Is<TransactionTypeEntity>(
                tm => tm.TypeName == request.TypeName
            ));
        }
    }
}

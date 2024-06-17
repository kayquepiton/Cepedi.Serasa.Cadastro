using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.TransactionType
{
    public class GetTransactionTypeByIdRequestHandlerTests
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository = Substitute.For<ITransactionTypeRepository>();
        private readonly ILogger<GetTransactionTypeByIdRequestHandler> _logger = Substitute.For<ILogger<GetTransactionTypeByIdRequestHandler>>();
        private readonly GetTransactionTypeByIdRequestHandler _sut;

        public GetTransactionTypeByIdRequestHandlerTests()
        {
            _sut = new GetTransactionTypeByIdRequestHandler(_logger, _transactionTypeRepository);
        }

        [Fact]
        public async Task Handle_WhenGetExistingTransactionType_ShouldReturnTransactionType()
        {
            // Arrange
            var transactionTypeId = 1;

            var existingTransactionType = new TransactionTypeEntity
            {
                Id = transactionTypeId,
                TypeName = "Purchase"
            };

            _transactionTypeRepository.GetTransactionTypeAsync(transactionTypeId)
                                      .Returns(Task.FromResult(existingTransactionType));

            var request = new GetTransactionTypeByIdRequest { Id = transactionTypeId };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            result.Value.Id.Should().Be(existingTransactionType.Id);
            result.Value.TypeName.Should().Be(existingTransactionType.TypeName);

            // Verify if the method on the repository was called correctly
            await _transactionTypeRepository.Received(1).GetTransactionTypeAsync(transactionTypeId);
        }

        [Fact]
        public async Task Handle_WhenGetNonExistingTransactionType_ShouldReturnError()
        {
            // Arrange
            var nonExistingTransactionTypeId = 99;

            _transactionTypeRepository.GetTransactionTypeAsync(nonExistingTransactionTypeId)
                                      .Returns(Task.FromResult<TransactionTypeEntity>(null));

            var request = new GetTransactionTypeByIdRequest { Id = nonExistingTransactionTypeId };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.InvalTransactionIdIdType);

            // Verify if the method on the repository was called correctly
            await _transactionTypeRepository.Received(1).GetTransactionTypeAsync(nonExistingTransactionTypeId);
        }
    }
}

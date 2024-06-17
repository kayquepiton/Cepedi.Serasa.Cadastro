using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.TransactionType
{
    public class DeleteTransactionTypeRequestHandlerTests
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository = Substitute.For<ITransactionTypeRepository>();
        private readonly ILogger<DeleteTransactionTypeByIdRequestHandler> _logger = Substitute.For<ILogger<DeleteTransactionTypeByIdRequestHandler>>();
        private readonly DeleteTransactionTypeByIdRequestHandler _sut;

        public DeleteTransactionTypeRequestHandlerTests()
        {
            _sut = new DeleteTransactionTypeByIdRequestHandler(_logger, _transactionTypeRepository);
        }

        [Fact]
        public async Task Handle_WhenDeleteTransactionType_ShouldReturnSuccess()
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

            // Act
            var result = await _sut.Handle(new DeleteTransactionTypeByIdRequest { Id = transactionTypeId }, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<DeleteTransactionTypeByIdResponse>>()
                .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(existingTransactionType.Id);
            result.Value.TypeName.Should().Be(existingTransactionType.TypeName);

            // Verify if the method on the repository was called correctly
            await _transactionTypeRepository.Received(1).GetTransactionTypeAsync(transactionTypeId);
            await _transactionTypeRepository.Received(1).DeleteTransactionTypeAsync(transactionTypeId);
        }

        [Fact]
        public async Task Handle_WhenDeleteNonExistingTransactionType_ShouldReturnFailure()
        {
            // Arrange
            var nonExistingTransactionTypeId = 99;

            _transactionTypeRepository.GetTransactionTypeAsync(nonExistingTransactionTypeId)
                                      .Returns(Task.FromResult<TransactionTypeEntity>(null));

            // Act
            var result = await _sut.Handle(new DeleteTransactionTypeByIdRequest { Id = nonExistingTransactionTypeId }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull(); // Verifies that the result is not null
            result.IsSuccess.Should().BeFalse(); // Verifies that the operation failed
            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.InvalTransactionIdIdType);

            // Verify if the method on the repository was called correctly
            await _transactionTypeRepository.Received(1).GetTransactionTypeAsync(nonExistingTransactionTypeId);
            await _transactionTypeRepository.DidNotReceive().DeleteTransactionTypeAsync(Arg.Any<int>());
        }
    }
}

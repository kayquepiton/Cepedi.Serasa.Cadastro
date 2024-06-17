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
    public class UpdateTransactionTypeRequestHandlerTests
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository = Substitute.For<ITransactionTypeRepository>();
        private readonly ILogger<UpdateTransactionTypeRequestHandler> _logger = Substitute.For<ILogger<UpdateTransactionTypeRequestHandler>>();
        private readonly UpdateTransactionTypeRequestHandler _sut;

        public UpdateTransactionTypeRequestHandlerTests()
        {
            _sut = new UpdateTransactionTypeRequestHandler(_transactionTypeRepository, _logger);
        }

        [Fact]
        public async Task Handle_WhenUpdateTransactionType_ShouldReturnSuccess()
        {
            // Arrange
            var request = new UpdateTransactionTypeRequest
            {
                Id = 1,
                TypeName = "New Sale"
            };

            var existingTransactionType = new TransactionTypeEntity
            {
                Id = request.Id,
                TypeName = "Sale"
            };

            _transactionTypeRepository.GetTransactionTypeAsync(request.Id).Returns(Task.FromResult(existingTransactionType));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<UpdateTransactionTypeResponse>>()
                .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(existingTransactionType.Id);
            result.Value.TypeName.Should().Be(request.TypeName);

            await _transactionTypeRepository.Received(1).GetTransactionTypeAsync(request.Id);
            await _transactionTypeRepository.Received(1).UpdateTransactionTypeAsync(Arg.Is<TransactionTypeEntity>(
                tt => tt.Id == request.Id &&
                      tt.TypeName == request.TypeName
            ));
        }

        [Fact]
        public async Task Handle_WhenTransactionTypeDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var request = new UpdateTransactionTypeRequest
            {
                Id = 1,
                TypeName = "New Sale"
            };

            _transactionTypeRepository.GetTransactionTypeAsync(request.Id).Returns(Task.FromResult<TransactionTypeEntity>(null));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<UpdateTransactionTypeResponse>>()
                .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.InvalTransactionIdIdType);
        }
    }
}

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

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Transaction
{
    public class CreateTransactionRequestHandlerTests
    {
        private readonly ITransactionRepository _transactionRepository = Substitute.For<ITransactionRepository>();
        private readonly IPersonRepository _personRepository = Substitute.For<IPersonRepository>();
        private readonly ITransactionTypeRepository _transactionTypeRepository = Substitute.For<ITransactionTypeRepository>();
        private readonly ILogger<CreateTransactionRequestHandler> _logger = Substitute.For<ILogger<CreateTransactionRequestHandler>>();
        private readonly CreateTransactionRequestHandler _sut;

        public CreateTransactionRequestHandlerTests()
        {
            _sut = new CreateTransactionRequestHandler(_logger, _transactionRepository, _personRepository, _transactionTypeRepository);
        }

        [Fact]
        public async Task Handle_WhenCreateTransaction_ShouldReturnSuccess()
        {
            // Arrange
            var transactionType = new TransactionTypeEntity
            {
                Id = 1,
                TypeName = "Purchase"
            };

            var person = new PersonEntity
            {
                Id = 1,
                Name = "João",
                CPF = "12345678901"
            };

            var request = new CreateTransactionRequest
            {
                TransactionTypeId = transactionType.Id,
                PersonId = person.Id,
                EstablishmentName = "Example Store",
                Value = 100.0m
            };

            var createdTransaction = new TransactionEntity
            {
                Id = 1,
                TransactionTypeId = transactionType.Id,
                PersonId = person.Id,
                DateTime = DateTime.UtcNow,
                EstablishmentName = request.EstablishmentName,
                Value = request.Value,
            };

            _personRepository.GetPersonAsync(request.PersonId).Returns(Task.FromResult(person));
            _transactionTypeRepository.GetTransactionTypeAsync(request.TransactionTypeId).Returns(Task.FromResult(transactionType));
            _transactionRepository.CreateTransactionAsync(Arg.Any<TransactionEntity>()).Returns(createdTransaction);

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<CreateTransactionResponse>>()
                  .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.TransactionTypeId.Should().Be(transactionType.Id);
            result.Value.PersonId.Should().Be(person.Id);
            result.Value.EstablishmentName.Should().Be(request.EstablishmentName);
            result.Value.Value.Should().Be(request.Value);

            await _personRepository.Received(1).GetPersonAsync(request.PersonId);
            await _transactionTypeRepository.Received(1).GetTransactionTypeAsync(request.TransactionTypeId);
            await _transactionRepository.Received(1).CreateTransactionAsync(Arg.Any<TransactionEntity>());
        }

        [Fact]
        public async Task Handle_WhenTransactionTypeDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var transactionTypeId = 999; // Invalid ID that does not exist in the repository
            var person = new PersonEntity
            {
                Id = 1,
                Name = "João",
                CPF = "12345678901"
            };

            var request = new CreateTransactionRequest
            {
                TransactionTypeId = transactionTypeId,
                PersonId = person.Id,
                EstablishmentName = "Example Store",
                Value = 100.0m
            };

            _personRepository.GetPersonAsync(request.PersonId).Returns(Task.FromResult(person));
            _transactionTypeRepository.GetTransactionTypeAsync(request.TransactionTypeId).Returns(Task.FromResult<TransactionTypeEntity>(null));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<CreateTransactionResponse>>()
                .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.InvalTransactionIdIdType);

            await _personRepository.Received(1).GetPersonAsync(request.PersonId);
            await _transactionTypeRepository.Received(1).GetTransactionTypeAsync(request.TransactionTypeId);
            await _transactionRepository.DidNotReceiveWithAnyArgs().CreateTransactionAsync(Arg.Any<TransactionEntity>());
        }

        [Fact]
        public async Task Handle_WhenPersonDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var transactionType = new TransactionTypeEntity
            {
                Id = 1,
                TypeName = "Purchase"
            };

            var nonExistingPersonId = 999; // Invalid ID that does not exist in the repository
            var request = new CreateTransactionRequest
            {
                TransactionTypeId = transactionType.Id,
                PersonId = nonExistingPersonId,
                EstablishmentName = "Example Store",
                Value = 100.0m
            };

            _personRepository.GetPersonAsync(request.PersonId).Returns(Task.FromResult<PersonEntity>(null));
            _transactionTypeRepository.GetTransactionTypeAsync(request.TransactionTypeId).Returns(Task.FromResult(transactionType));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<CreateTransactionResponse>>()
                .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.InvalidPersonId);

            await _personRepository.Received(1).GetPersonAsync(request.PersonId);
            await _transactionTypeRepository.DidNotReceive().GetTransactionTypeAsync(request.TransactionTypeId);
            await _transactionRepository.DidNotReceiveWithAnyArgs().CreateTransactionAsync(Arg.Any<TransactionEntity>());
        }
    }
}

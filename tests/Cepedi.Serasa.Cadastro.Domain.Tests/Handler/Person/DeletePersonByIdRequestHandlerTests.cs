using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OperationResult;
namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Person
{
    public class DeletePersonByIdRequestHandlerTests
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<DeletePersonByIdRequestHandler> _logger;
        private readonly DeletePersonByIdRequestHandler _sut;

        public DeletePersonByIdRequestHandlerTests()
        {
            _personRepository = Substitute.For<IPersonRepository>();
            _logger = Substitute.For<ILogger<DeletePersonByIdRequestHandler>>();
            _sut = new DeletePersonByIdRequestHandler(_logger, _personRepository);
        }

        [Fact]
        public async Task WhenDeleteExistingPersonById_ShouldReturnSuccess()
        {
            // Arrange
            var request = new DeletePersonByIdRequest { Id = 1 };

            var person = new PersonEntity
            {
                Id = 1,
                Name = "Carlos Matos",
                CPF = "86088154004"
            };

            _personRepository.GetPersonAsync(request.Id).Returns(Task.FromResult(person));
            _personRepository.DeletePersonAsync(request.Id).Returns(Task.FromResult(person));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Should().BeOfType<Result<DeletePersonByIdResponse>>();
            result.Value.Id.Should().Be(person.Id);
            result.Value.Name.Should().Be(person.Name);
            result.Value.CPF.Should().Be(person.CPF);

            await _personRepository.Received(1).GetPersonAsync(request.Id);
            await _personRepository.Received(1).DeletePersonAsync(request.Id);
        }

        [Fact]
        public async Task WhenDeleteNonExistingPersonById_ShouldReturnFailure()
        {
            // Arrange
            var request = new DeletePersonByIdRequest { Id = 10 };

            _personRepository.DeletePersonAsync(request.Id).ReturnsNull();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<DeletePersonByIdResponse>>();
            result.IsSuccess.Should().BeFalse();

            await _personRepository.Received(1).GetPersonAsync(request.Id);
        }
    }
}

using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Shared.Validators.Person;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Person
{
    public class CreatePersonRequestHandlerTests
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<CreatePersonRequestHandler> _logger;
        private readonly CreatePersonRequestHandler _sut;

        public CreatePersonRequestHandlerTests()
        {
            _personRepository = Substitute.For<IPersonRepository>();
            _logger = Substitute.For<ILogger<CreatePersonRequestHandler>>();
            _sut = new CreatePersonRequestHandler(_personRepository, _logger);
        }

        [Fact]
        public async Task WhenCreatePerson_ShouldReturnSuccess()
        {
            // Arrange
            var personRequest = new CreatePersonRequest
            {
                Name = "Fernando Lima",
                CPF = "43669795006"
            };

            var person = new PersonEntity
            {
                Name = personRequest.Name,
                CPF = personRequest.CPF
            };

            _personRepository.CreatePersonAsync(Arg.Is<PersonEntity>(p => p.Name == personRequest.Name && p.CPF == personRequest.CPF))
                .Returns(person);

            // Act
            var result = await _sut.Handle(personRequest, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Result<CreatePersonResponse>>();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Name.Should().Be(personRequest.Name);
            result.Value.CPF.Should().Be(personRequest.CPF);

            await _personRepository.Received(1)
                .CreatePersonAsync(Arg.Is<PersonEntity>(p => p.Name == personRequest.Name && p.CPF == personRequest.CPF));
        }

        [Fact]
        public async Task WhenCreatePersonWithInvalidData_ShouldReturnError()
        {
            // Arrange
            var personRequest = new CreatePersonRequest
            {
                Name = "Ze",
                CPF = "123"
            };

            var validator = new CreatePersonRequestValidator();

            // Act
            var validationResult = validator.Validate(personRequest);
            var result = await _sut.Handle(personRequest, CancellationToken.None);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            result.Should().NotBeNull();
            result.Should().BeOfType<Result<CreatePersonResponse>>();
            result.IsSuccess.Should().BeFalse();

            await _personRepository.DidNotReceive()
                .CreatePersonAsync(Arg.Any<PersonEntity>());
        }
    }
}

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
    public class UpdatePersonRequestHandlerTests
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<UpdatePersonRequestHandler> _logger;
        private readonly UpdatePersonRequestHandler _sut;

        public UpdatePersonRequestHandlerTests()
        {
            _personRepository = Substitute.For<IPersonRepository>();
            _logger = Substitute.For<ILogger<UpdatePersonRequestHandler>>();
            _sut = new UpdatePersonRequestHandler(_personRepository, _logger);
        }

        [Fact]
        public async Task WhenUpdatingPerson_ShouldReturnSuccess()
        {
            var request = new UpdatePersonRequest
            {
                Id = 1,
                Name = "Carlos Matos",
                CPF = "86088154004"
            };

            var existingPersonFromDb = new PersonEntity
            {
                Id = 1,
                Name = "Carlos",
                CPF = "86088154002"
            };

            var updatedPerson = new PersonEntity
            {
                Id = request.Id,
                Name = request.Name,
                CPF = request.CPF
            };

            _personRepository.GetPersonAsync(request.Id).Returns(Task.FromResult(existingPersonFromDb));
            _personRepository.UpdatePersonAsync(existingPersonFromDb).Returns(Task.FromResult(updatedPerson));

            var result = await _sut.Handle(request, CancellationToken.None);

            result.Should().BeOfType<Result<UpdatePersonResponse>>().Which.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(request.Id);
            result.Value.Name.Should().Be(request.Name);
            result.Value.CPF.Should().Be(request.CPF);

            await _personRepository.Received(1).GetPersonAsync(request.Id);
            await _personRepository.Received(1).UpdatePersonAsync(existingPersonFromDb);
        }

        [Fact]
        public async Task WhenUpdatingPersonWithNonExistingId_ShouldReturnError()
        {
            var request = new UpdatePersonRequest
            {
                Id = 50,
                Name = "Zé",
                CPF = "1234"
            };

            _personRepository.GetPersonAsync(request.Id).ReturnsNull();

            var result = await _sut.Handle(request, CancellationToken.None);

            await _personRepository.Received(1).GetPersonAsync(request.Id);

            result.Should().BeOfType<Result<UpdatePersonResponse>>();
            result.IsSuccess.Should().BeFalse();
        }
    }
}

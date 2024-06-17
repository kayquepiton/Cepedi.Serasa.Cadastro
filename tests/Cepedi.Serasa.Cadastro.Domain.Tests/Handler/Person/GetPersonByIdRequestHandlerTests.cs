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
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Person
{
    public class GetPersonByIdRequestHandlerTests
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<GetPersonByIdRequestHandler> _logger;
        private readonly GetPersonByIdRequestHandler _sut;

        public GetPersonByIdRequestHandlerTests()
        {
            _personRepository = Substitute.For<IPersonRepository>();
            _logger = Substitute.For<ILogger<GetPersonByIdRequestHandler>>();
            _sut = new GetPersonByIdRequestHandler(_personRepository, _logger);
        }

        [Fact]
        public async Task WhenGetExistingPersonById_ShouldReturnPersonResponse()
        {
            // Arrange
            var request = new GetPersonByIdRequest { Id = 1 };

            var person = new PersonEntity
            {
                Id = request.Id,
                Name = "Carlos Matos",
                CPF = "86088154004"
            };

            _personRepository.GetPersonAsync(request.Id).Returns(Task.FromResult(person));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Result<GetPersonByIdResponse>>();
            result.Value.Id.Should().Be(request.Id);
            result.Value.Name.Should().Be(person.Name);
            result.Value.CPF.Should().Be(person.CPF);

            await _personRepository.Received(1).GetPersonAsync(request.Id);
        }

        [Fact]
        public async Task WhenGetNonExistingPersonById_ShouldReturnNull()
        {
            // Arrange
            var request = new GetPersonByIdRequest { Id = 100 };

            _personRepository.GetPersonAsync(request.Id).ReturnsNull();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();

            await _personRepository.Received(1).GetPersonAsync(request.Id);
        }
    }
}

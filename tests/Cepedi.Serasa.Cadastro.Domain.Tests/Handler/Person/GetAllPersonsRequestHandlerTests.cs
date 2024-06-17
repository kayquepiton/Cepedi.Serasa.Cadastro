using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Person
{
    public class GetAllPersonsRequestHandlerTests
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<GetAllPersonsRequestHandler> _logger;
        private readonly GetAllPersonsRequestHandler _sut;

        public GetAllPersonsRequestHandlerTests()
        {
            _personRepository = Substitute.For<IPersonRepository>();
            _logger = Substitute.For<ILogger<GetAllPersonsRequestHandler>>();
            _sut = new GetAllPersonsRequestHandler(_personRepository, _logger);
        }

        [Fact]
        public async Task WhenGetAllPersons_ShouldReturnListOfPersons()
        {
            // Arrange
            var personsInDatabase = new List<PersonEntity>
            {
                new PersonEntity { Id = 1, Name = "Carlos Matos", CPF = "86088154004" },
                new PersonEntity { Id = 2, Name = "Joana Andrade", CPF = "45072726029" },
                new PersonEntity { Id = 3, Name = "Felipe Meira", CPF = "54284570072" }
            };

            _personRepository.GetAllPersonsAsync().Returns(Task.FromResult(personsInDatabase));

            var request = new GetAllPersonsRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(personsInDatabase.Count);

            var resultList = result.Value.ToList();
            for (int i = 0; i < resultList.Count; i++)
            {
                resultList[i].Id.Should().Be(personsInDatabase[i].Id);
                resultList[i].Name.Should().Be(personsInDatabase[i].Name);
                resultList[i].CPF.Should().Be(personsInDatabase[i].CPF);
            }

            await _personRepository.Received(1).GetAllPersonsAsync();
        }

        [Fact]
        public async Task WhenNoPersonsRegistered_ShouldReturnEmptyList()
        {
            // Arrange
            var personsInDatabase = new List<PersonEntity>();

            _personRepository.GetAllPersonsAsync().Returns(Task.FromResult(personsInDatabase));

            var request = new GetAllPersonsRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();

            await _personRepository.Received(1).GetAllPersonsAsync();
        }
    }
}

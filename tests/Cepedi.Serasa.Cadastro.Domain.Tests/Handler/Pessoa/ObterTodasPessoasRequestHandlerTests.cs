using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Person;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Person;
public class ObterTodasPersonsRequestHandlerTests
{
    private readonly IPersonRepository _PersonsRepository;
    private readonly ILogger<ObterTodasPersonsRequestHandler> _logger;
    private readonly ObterTodasPersonsRequestHandler _sut;

    public ObterTodasPersonsRequestHandlerTests()
    {
        _PersonsRepository = Substitute.For<IPersonRepository>();
        _logger = Substitute.For<ILogger<ObterTodasPersonsRequestHandler>>();
        _sut = new ObterTodasPersonsRequestHandler(_PersonsRepository, _logger);
    }

    [Fact]
    public async Task QuandoObterTodasPersonsDeveRetornarListaPersons()
    {
        //Arrange
        var PersonsNoBanco = new List<PersonEntity>
        {
            new PersonEntity {Id = 1, Name = "Carlos Matos", CPF = "86088154004"},
            new PersonEntity {Id = 2, Name = "Joana Andrade", CPF = "45072726029"},
            new PersonEntity {Id = 3, Name = "Felipe Meira", CPF = "54284570072"}
        };

        _PersonsRepository.ObterPersonsAsync().Returns(Task.FromResult(PersonsNoBanco));

        var request = new ObterTodasPersonsRequest();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value.Should().HaveCount(PersonsNoBanco.Count);

        var resultList = result.Value.ToList();
        for(int i = 0; i < resultList.Count(); i++)
        {
            resultList[i].Id.Should().Be(PersonsNoBanco[i].Id);
            resultList[i].Name.Should().Be(PersonsNoBanco[i].Name);
            resultList[i].CPF.Should().Be(PersonsNoBanco[i].CPF);
        }

        await _PersonsRepository.Received(1).ObterPersonsAsync();
    }

    [Fact]
    public async Task QuandoNaoExistirPersonsCadastradasDeveRetornarListaVazia()
    {
        //Arrange
        var PersonsNoBanco = new List<PersonEntity>();

        _PersonsRepository.ObterPersonsAsync().Returns(Task.FromResult(PersonsNoBanco));

        var request = new ObterTodasPersonsRequest();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();

        await _PersonsRepository.Received(1).ObterPersonsAsync();
    }
}

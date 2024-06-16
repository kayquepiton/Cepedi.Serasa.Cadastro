using Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Consulta;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Consulta;
public class ObterConsultaRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly ILogger<ObterConsultaRequestHandler> _logger = Substitute.For<ILogger<ObterConsultaRequestHandler>>();
    private readonly ObterConsultaRequestHandler _sut;

    public ObterConsultaRequestHandlerTests()
    {
        _sut = new ObterConsultaRequestHandler(_consultaRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoObterConsultaExistente_DeveRetornarConsulta()
    {

        //Arrange

        var idConsulta = 1;

        var consultaExistente = new QueryEntity
        {
            Id = idConsulta,
            Status = true,
            Data = DateTime.UtcNow.AddDays(-1),
            IdPerson = 1
        };

        _consultaRepository.ObterConsultaAsync(idConsulta)
                            .Returns(Task.FromResult(consultaExistente));

        var request = new ObterConsultaRequest { Id = idConsulta };

        //Act

        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert

        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        result.Value.Id.Should().Be(consultaExistente.Id);
        result.Value.Status.Should().Be(consultaExistente.Status);
        result.Value.Data.Should().Be(consultaExistente.Data);
        result.Value.IdPerson.Should().Be(consultaExistente.IdPerson);

        await _consultaRepository.Received(1).ObterConsultaAsync(idConsulta);

    }

    [Fact]
    public async Task Handle_QuandoObterConsultaInexistente_DeveRetornarNulo(){
         // Arrange
            var idConsultaInexistente = 123;

            _consultaRepository.ObterConsultaAsync(idConsultaInexistente)
                                    .Returns(Task.FromResult<QueryEntity>(null));

            var request = new ObterConsultaRequest { Id = idConsultaInexistente };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();

            await _consultaRepository.Received(1).ObterConsultaAsync(idConsultaInexistente);
    }


}

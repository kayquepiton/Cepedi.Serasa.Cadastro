/*using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests;
using Cepedi.Serasa.Cadastro.Shared.Responses;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests;

public class CriarUsuarioRequestHandlerTests
{
    private readonly IUsuarioRepository _usuarioRepository =
    Substitute.For<IUsuarioRepository>();
    private readonly ILogger<CriarUsuarioRequestHandler> _logger = Substitute.For<ILogger<CriarUsuarioRequestHandler>>();
    private readonly CriarUsuarioRequestHandler _sut;

    public CriarUsuarioRequestHandlerTests()
    {
        _sut = new CriarUsuarioRequestHandler(_usuarioRepository, _logger);
    }

    [Fact]
    public async Task CriarUsuarioAsync_QuandoCriar_DeveRetornarSucesso()
    {
        //Arrange 
        var usuario = new CriarUsuarioRequest { Name= "Denis" };
        _usuarioRepository.CriarUsuarioAsync(It.IsAny<UserEntity>())
            .ReturnsForAnyArgs(new UserEntity());

        //Act
        var result = await _sut.Handle(usuario, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<CriarUsuarioResponse>>().Which
            .Value.Name.Should().Be(usuario.Name);
        result.Should().BeOfType<Result<CriarUsuarioResponse>>().Which
            .Value.Name.Should().NotBeEmpty();
    }

}*/

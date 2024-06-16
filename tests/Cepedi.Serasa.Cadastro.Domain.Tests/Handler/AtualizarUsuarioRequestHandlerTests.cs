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

public class AtualizarUsuarioRequestHandlerTests
{
    private readonly IUsuarioRepository _usuarioRepository =
    Substitute.For<IUsuarioRepository>();
    private readonly ILogger<AtualizarUsuarioRequestHandler> _logger = Substitute.For<ILogger<AtualizarUsuarioRequestHandler>>();
    private readonly AtualizarUsuarioRequestHandler _sut;

    public AtualizarUsuarioRequestHandlerTests()
    {
        _sut = new AtualizarUsuarioRequestHandler(_usuarioRepository, _logger);
    }

    [Fact]
    public async Task AtualizarUsuarioAsync_QuandoAtualizar_DeveRetornarSucesso()
    {
        //Arrange 
        var usuario = new AtualizarUsuarioRequest { Name= "Denis" };
        var UserEntity = new UserEntity { Name = "Denis" };
        _usuarioRepository.ObterUsuarioAsync(It.IsAny<int>()).ReturnsForAnyArgs(new UserEntity());
        _usuarioRepository.AtualizarUsuarioAsync(It.IsAny<UserEntity>())
            .ReturnsForAnyArgs(UserEntity);

        //Act
        var result = await _sut.Handle(usuario, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<AtualizarUsuarioResponse>>().Which
            .Value.Name.Should().Be(usuario.Name);

        result.Should().BeOfType<Result<AtualizarUsuarioResponse>>().Which
            .Value.Name.Should().NotBeEmpty();
    }

}*/

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

public class UpdateUsuarioRequestHandlerTests
{
    private readonly IUsuarioRepository _usuarioRepository =
    Substitute.For<IUsuarioRepository>();
    private readonly ILogger<UpdateUsuarioRequestHandler> _logger = Substitute.For<ILogger<UpdateUsuarioRequestHandler>>();
    private readonly UpdateUsuarioRequestHandler _sut;

    public UpdateUsuarioRequestHandlerTests()
    {
        _sut = new UpdateUsuarioRequestHandler(_usuarioRepository, _logger);
    }

    [Fact]
    public async Task UpdateUsuarioAsync_QuandoUpdate_DeveRetornarSucesso()
    {
        //Arrange 
        var usuario = new UpdateUsuarioRequest { Name= "Denis" };
        var UserEntity = new UserEntity { Name = "Denis" };
        _usuarioRepository.GetUsuarioAsync(It.IsAny<int>()).ReturnsForAnyArgs(new UserEntity());
        _usuarioRepository.UpdateUsuarioAsync(It.IsAny<UserEntity>())
            .ReturnsForAnyArgs(UserEntity);

        //Act
        var result = await _sut.Handle(usuario, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<UpdateUsuarioResponse>>().Which
            .Value.Name.Should().Be(usuario.Name);

        result.Should().BeOfType<Result<UpdateUsuarioResponse>>().Which
            .Value.Name.Should().NotBeEmpty();
    }

}*/

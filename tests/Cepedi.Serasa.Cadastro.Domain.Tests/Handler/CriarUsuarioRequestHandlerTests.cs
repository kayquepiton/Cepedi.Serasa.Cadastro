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

public class CreateUsuarioRequestHandlerTests
{
    private readonly IUsuarioRepository _usuarioRepository =
    Substitute.For<IUsuarioRepository>();
    private readonly ILogger<CreateUsuarioRequestHandler> _logger = Substitute.For<ILogger<CreateUsuarioRequestHandler>>();
    private readonly CreateUsuarioRequestHandler _sut;

    public CreateUsuarioRequestHandlerTests()
    {
        _sut = new CreateUsuarioRequestHandler(_usuarioRepository, _logger);
    }

    [Fact]
    public async Task CreateUsuarioAsync_QuandoCreate_DeveRetornarSucesso()
    {
        //Arrange 
        var usuario = new CreateUsuarioRequest { Name= "Denis" };
        _usuarioRepository.CreateUsuarioAsync(It.IsAny<UserEntity>())
            .ReturnsForAnyArgs(new UserEntity());

        //Act
        var result = await _sut.Handle(usuario, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<CreateUsuarioResponse>>().Which
            .Value.Name.Should().Be(usuario.Name);
        result.Should().BeOfType<Result<CreateUsuarioResponse>>().Which
            .Value.Name.Should().NotBeEmpty();
    }

}*/

/*using Cepedi.Serasa.Cadastro.Api.Controllers;
using Cepedi.Serasa.Cadastro.Shared.Requests;
using Cepedi.Serasa.Cadastro.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Api.Tests
{
    public class LoginControllerTests
    {
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        private readonly ILogger<LoginController> _logger = Substitute.For<ILogger<LoginController>>();
        private readonly LoginController _sut;

        public LoginControllerTests()
        {
            _sut = new LoginController(_logger, _mediator);
        }

        [Fact]
        public async Task CriarUsuario_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new CriarUsuarioRequest { Name = "Denis" };
            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarUsuarioResponse(1, "")));

            // Act
            await _sut.CriarUsuarioAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }
    }
}*/

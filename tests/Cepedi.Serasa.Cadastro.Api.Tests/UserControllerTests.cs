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
    public class UsernameControllerTests
    {
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        private readonly ILogger<UsernameController> _logger = Substitute.For<ILogger<UsernameController>>();
        private readonly UsernameController _sut;

        public UsernameControllerTests()
        {
            _sut = new UsernameController(_logger, _mediator);
        }

        [Fact]
        public async Task CreateUsuario_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new CreateUsuarioRequest { Name = "Denis" };
            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CreateUsuarioResponse(1, "")));

            // Act
            await _sut.CreateUsuarioAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }
    }
}*/

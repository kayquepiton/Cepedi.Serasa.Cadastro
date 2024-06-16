using Cepedi.Serasa.Cadastro.Shared.Auth.Requests;
using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;

    public AuthController(ILogger<AuthController> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("signin")]
    [ProducesResponseType(typeof(AuthenticateUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthenticateUserResponse>> AuthenticateUserAsync(
        [FromBody] AuthenticateUserRequest request) => await SendCommand(request);

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshTokenAsync(
        [FromBody] RefreshTokenRequest request) => await SendCommand(request);

    [HttpPost("revoke")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request) => await SendCommand(request);
}

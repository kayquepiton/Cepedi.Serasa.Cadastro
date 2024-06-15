using Cepedi.Serasa.Cadastro.Shared.Auth.Requests;
using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;
using Cepedi.Serasa.Cadastro.Shared.Exececoes;
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
    [ProducesResponseType(typeof(AutenticarUsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AutenticarUsuarioResponse>> AutenticarUsuarioAsync(
        [FromBody] AutenticarUsuarioRequest request) => await SendCommand(request);

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AtualizarTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AtualizarTokenResponse>> RefreshTokenAsync(
        [FromBody] AtualizarTokenRequest request) => await SendCommand(request);

    [HttpPost("revoke")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RevokeToken([FromBody] RevogarTokenRequest request) => await SendCommand(request);
}


using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.User.Requests;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator) : base(mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllUsersResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<GetAllUsersResponse>>> GetAllUsersAsync()
            => await SendCommand(new GetAllUsersRequest());

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateUserResponse>> CreateUserAsync(
            [FromBody] CreateUserRequest request) => await SendCommand(request);

        [HttpPut]
        [ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUserAsync(
            [FromBody] UpdateUserRequest request) => await SendCommand(request);

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetUserByIdResponse>> GetUserByIdAsync(
            [FromRoute] GetUserByIdRequest request) => await SendCommand(request);

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(DeleteUserByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<DeleteUserByIdResponse>> DeleteUserAsync(
            [FromRoute] DeleteUserByIdRequest request) => await SendCommand(request);
    }
}

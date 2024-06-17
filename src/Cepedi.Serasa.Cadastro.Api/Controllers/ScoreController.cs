using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : BaseController
    {
        private readonly ILogger<ScoreController> _logger;
        private readonly IMediator _mediator;

        public ScoreController(ILogger<ScoreController> logger, IMediator mediator) : base(mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllScoresResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<GetAllScoresResponse>>> GetAllScoresAsync()
            => await SendCommand(new GetAllScoresRequest());

        [HttpPost]
        [ProducesResponseType(typeof(CreateScoreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateScoreResponse>> CreateScoreAsync(
            [FromBody] CreateScoreRequest request) => await SendCommand(request);

        [HttpPut]
        [ProducesResponseType(typeof(UpdateScoreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<UpdateScoreResponse>> UpdateScoreAsync(
            [FromBody] UpdateScoreRequest request) => await SendCommand(request);

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(GetScoreByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetScoreByIdResponse>> GetScoreByIdAsync(
            [FromRoute] GetScoreByIdRequest request) => await SendCommand(request);

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(DeleteScoreByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<DeleteScoreByIdResponse>> DeleteScoreAsync(
            [FromRoute] DeleteScoreByIdRequest request) => await SendCommand(request);
    }
}

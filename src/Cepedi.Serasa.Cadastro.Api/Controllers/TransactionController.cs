using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : BaseController
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IMediator _mediator;

        public TransactionController(ILogger<TransactionController> logger, IMediator mediator) : base(mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllTransactionsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<GetAllTransactionsResponse>>> GetAllTransactionsAsync()
            => await SendCommand(new GetAllTransactionsRequest());

        [HttpPost]
        [ProducesResponseType(typeof(CreateTransactionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTransactionResponse>> CreateTransactionAsync(
            [FromBody] CreateTransactionRequest request) => await SendCommand(request);

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTransactionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<UpdateTransactionResponse>> UpdateTransactionAsync(
            [FromBody] UpdateTransactionRequest request) => await SendCommand(request);

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(GetTransactionByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTransactionByIdResponse>> GetTransactionAsync(
            [FromRoute] GetTransactionByIdRequest request) => await SendCommand(request);

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(DeleteTransactionByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<DeleteTransactionByIdResponse>> DeleteTransactionAsync(
            [FromRoute] DeleteTransactionByIdRequest request) => await SendCommand(request);
    }
}

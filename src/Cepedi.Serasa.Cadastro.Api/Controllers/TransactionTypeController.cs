using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class TransactionTypeController : BaseController
{
    private readonly ILogger<TransactionTypeController> _logger;
    private readonly IMediator _mediator;
    
    public TransactionTypeController(ILogger<TransactionTypeController> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetAllTransactionTypesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<GetAllTransactionTypesResponse>>> GetAllTransactionTypesAsync()
        => await SendCommand(new GetAllTransactionTypesRequest());

    [HttpPost]
    [ProducesResponseType(typeof(CreateTransactionTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateTransactionTypeResponse>> CreateTransactionTypeAsync(
        [FromBody] CreateTransactionTypeRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(UpdateTransactionTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateTransactionTypeResponse>> UpdateTransactionTypeAsync(
        [FromBody] UpdateTransactionTypeRequest request) => await SendCommand(request);

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(GetTransactionTypeByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetTransactionTypeByIdResponse>> GetTransactionTypeAsync(
        [FromRoute] GetTransactionTypeByIdRequest request) => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeleteTransactionTypeByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeleteTransactionTypeByIdResponse>> DeleteTransactionTypeAsync(
        [FromRoute] DeleteTransactionTypeByIdRequest request) => await SendCommand(request);
}

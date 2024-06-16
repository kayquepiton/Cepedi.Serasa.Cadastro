using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class QueryController : BaseController
{
    private readonly ILogger<QueryController> _logger;
    private readonly IMediator _mediator;

    public QueryController(ILogger<QueryController> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetAllQueriesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<GetAllQueriesResponse>>> GetAllQueriesAsync()
        => await SendCommand(new GetAllQueriesRequest());

    [HttpPost]
    [ProducesResponseType(typeof(CreateQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateQueryResponse>> CreateQueryAsync(
        [FromBody] CreateQueryRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(UpdateQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateQueryResponse>> UpdateQueryAsync(
        [FromBody] UpdateQueryRequest request) => await SendCommand(request);

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(GetQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetQueryResponse>> GetQueryAsync(
        [FromRoute] GetQueryRequest request) => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeleteQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeleteQueryResponse>> DeleteQueryAsync(
        [FromRoute] DeleteQueryRequest request) => await SendCommand(request);
}

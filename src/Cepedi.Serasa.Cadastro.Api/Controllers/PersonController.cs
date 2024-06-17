using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PersonController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<PersonController> _logger;

    public PersonController(IMediator mediator, ILogger<PersonController> logger) : base(mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetPersonByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetPersonByIdResponse>>> GetAllPersonsAsync()
        => await SendCommand(new GetAllPersonsRequest());

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetPersonByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPersonByIdResponse>> GetPersonByIdAsync(
        [FromRoute] int id)
    {
        var request = new GetPersonByIdRequest { Id = id };
        return await SendCommand(request);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatePersonResponse>> CreatePersonAsync(
        [FromBody] CreatePersonRequest request)
        => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(UpdatePersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdatePersonResponse>> UpdatePersonAsync(
        [FromBody] UpdatePersonRequest request)
        => await SendCommand(request);

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(DeletePersonByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletePersonByIdResponse>> DeletePersonAsync(
        [FromRoute] DeletePersonByIdRequest request) => await SendCommand(request);
}

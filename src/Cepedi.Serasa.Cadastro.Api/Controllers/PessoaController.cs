﻿using Cepedi.Serasa.Cadastro.Shared.Exececoes;
using Cepedi.Serasa.Cadastro.Shared.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Shared.Responses.Pessoa;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PessoaController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<PessoaController> _logger;

    public PessoaController(IMediator mediator, ILogger<PessoaController> logger) : base(mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ObterPessoaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ObterPessoaResponse>>> ObterTodasPessoasAsync()
        => await SendCommand(new ObterTodasPessoasRequest());

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObterPessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ObterPessoaResponse>> ObterPessoaPorIdAsync(
        [FromRoute] int id)
    {
        var request = new ObterPessoaPorIdRequest { Id = id };
        return await SendCommand(request);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarPessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarPessoaResponse>> CriarPessoaAsync(
        [FromBody] CriarPessoaRequest request)
        => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarPessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarPessoaResponse>> AtualizarPessoaAsync(
        [FromBody] AtualizarPessoaRequest request)
        => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(ExcluirPessoaPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ExcluirPessoaPorIdResponse>> DeletarPessoaAsync(
        [FromRoute] ExcluirPessoaPorIdRequest request) => await SendCommand(request);
}
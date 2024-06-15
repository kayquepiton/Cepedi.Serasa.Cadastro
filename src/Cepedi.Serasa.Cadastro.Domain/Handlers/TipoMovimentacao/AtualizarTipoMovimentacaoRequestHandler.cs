﻿using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TipoMovimentacao;

public class AtualizarTipoMovimentacaoRequestHandler : IRequestHandler<AtualizarTipoMovimentacaoRequest, Result<AtualizarTipoMovimentacaoResponse>>
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<AtualizarTipoMovimentacaoRequestHandler> _logger;
    public AtualizarTipoMovimentacaoRequestHandler(ITipoMovimentacaoRepository tipoMovimentacaoRepository, ILogger<AtualizarTipoMovimentacaoRequestHandler> logger)
    {
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
        _logger = logger;
    }
    public async Task<Result<AtualizarTipoMovimentacaoResponse>> Handle(AtualizarTipoMovimentacaoRequest request, CancellationToken cancellationToken)
    {

        var tipoMovimentacaoEntity = await _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id);

        if (tipoMovimentacaoEntity == null)
        {
            return Result.Error<AtualizarTipoMovimentacaoResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdTipoMovimentacaoInvalido));
        }

        tipoMovimentacaoEntity.NomeTipo = request.NomeTipo;

        await _tipoMovimentacaoRepository.AtualizarTipoMovimentacaoAsync(tipoMovimentacaoEntity);

        return Result.Success(new AtualizarTipoMovimentacaoResponse(tipoMovimentacaoEntity.Id, tipoMovimentacaoEntity.NomeTipo));
    }
}
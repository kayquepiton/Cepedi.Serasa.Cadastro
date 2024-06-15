﻿using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TipoMovimentacao;

public class DeletarTipoMovimentacaoRequestHandler : IRequestHandler<DeletarTipoMovimentacaoRequest, Result<DeletarTipoMovimentacaoResponse>>
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<DeletarTipoMovimentacaoRequestHandler> _logger;
    public DeletarTipoMovimentacaoRequestHandler(ILogger<DeletarTipoMovimentacaoRequestHandler> logger, ITipoMovimentacaoRepository tipoMovimentacaoRepository)
    {
        _logger = logger;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
    }
    public async Task<Result<DeletarTipoMovimentacaoResponse>> Handle(DeletarTipoMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var tipoMovimentacaoEntity = await _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id);
        
        if (tipoMovimentacaoEntity == null) {
            return Result.Error<DeletarTipoMovimentacaoResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdTipoMovimentacaoInvalido));
        }
        
        await _tipoMovimentacaoRepository.DeletarTipoMovimentacaoAsync(tipoMovimentacaoEntity.Id);
        return Result.Success(new DeletarTipoMovimentacaoResponse(tipoMovimentacaoEntity.Id, tipoMovimentacaoEntity.NomeTipo));
    }
}
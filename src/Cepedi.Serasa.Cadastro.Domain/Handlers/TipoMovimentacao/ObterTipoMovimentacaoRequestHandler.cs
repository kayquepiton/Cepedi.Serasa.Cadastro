﻿﻿using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TipoMovimentacao;
public class ObterTipoMovimentacaoRequestHandler : IRequestHandler<ObterTipoMovimentacaoRequest, Result<ObterTipoMovimentacaoResponse>>
{
    private readonly ILogger<ObterTipoMovimentacaoRequestHandler> _logger;
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    public ObterTipoMovimentacaoRequestHandler(ILogger<ObterTipoMovimentacaoRequestHandler> logger, ITipoMovimentacaoRepository tipoMovimentacaoRepository){
        _logger = logger;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
    }
    public async Task<Result<ObterTipoMovimentacaoResponse>> Handle(ObterTipoMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var tipoMovimentacaoEntity = await _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id);
        return tipoMovimentacaoEntity == null
            ? Result.Error<ObterTipoMovimentacaoResponse>(new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdTipoMovimentacaoInvalido))
            : Result.Success(new ObterTipoMovimentacaoResponse(tipoMovimentacaoEntity.Id, tipoMovimentacaoEntity.NomeTipo));
    }
}
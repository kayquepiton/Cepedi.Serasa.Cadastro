﻿using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
public class DeletarTipoMovimentacaoRequest : IRequest<Result<DeletarTipoMovimentacaoResponse>>
{
    public int Id { get; set; }
}
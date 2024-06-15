﻿using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
public class ObterTipoMovimentacaoRequest : IRequest<Result<ObterTipoMovimentacaoResponse>>
{
    public int Id { get; set; }
}
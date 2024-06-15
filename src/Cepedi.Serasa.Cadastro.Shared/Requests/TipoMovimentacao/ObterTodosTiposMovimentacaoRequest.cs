﻿using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao
{
    public class ObterTodosTiposMovimentacaoRequest : IRequest<Result<List<ObterTodosTiposMovimentacaoResponse>>>
    {
    }
}

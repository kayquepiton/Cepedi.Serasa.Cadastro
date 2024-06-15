﻿using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score
{
    public class ObterTodosScoresRequest : IRequest<Result<List<ObterTodosScoresResponse>>>
    {
    }
}

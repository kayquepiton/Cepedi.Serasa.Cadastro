﻿using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score
{
    public class GetAllScoresRequest : IRequest<Result<List<GetAllScoresResponse>>>
    {
    }
}

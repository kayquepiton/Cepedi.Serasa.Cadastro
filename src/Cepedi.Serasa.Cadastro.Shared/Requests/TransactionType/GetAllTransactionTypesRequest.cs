﻿using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType
{
    public class GetAllTransactionTypesRequest : IRequest<Result<List<GetAllTransactionTypesResponse>>>
    {
    }
}

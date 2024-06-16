﻿using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType
{
    public class GetTransactionTypeRequest : IRequest<Result<GetTransactionTypeResponse>>
    {
        public int Id { get; set; }
    }
}

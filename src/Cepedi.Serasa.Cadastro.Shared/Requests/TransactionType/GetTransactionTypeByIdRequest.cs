﻿using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType
{
    public class GetTransactionTypeByIdRequest : IRequest<Result<GetTransactionTypeByIdResponse>>
    {
        public int Id { get; set; }
    }
}

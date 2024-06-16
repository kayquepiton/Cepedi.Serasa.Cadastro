﻿using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType
{
    public class DeleteTransactionTypeRequest : IRequest<Result<DeleteTransactionTypeResponse>>
    {
        public int Id { get; set; }
    }
}

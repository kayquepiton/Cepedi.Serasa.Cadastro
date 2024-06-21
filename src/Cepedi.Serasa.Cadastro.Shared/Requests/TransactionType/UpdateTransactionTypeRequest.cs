﻿using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
public class UpdateTransactionTypeRequest : IRequest<Result<UpdateTransactionTypeResponse>>, IValidate
{
    public required int Id { get; set; }
    public required string TypeName { get; set; } = default!;
}
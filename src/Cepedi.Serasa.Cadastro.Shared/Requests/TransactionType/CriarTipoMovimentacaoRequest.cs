using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
public class CreateTransactionTypeRequest : IRequest<Result<CreateTransactionTypeResponse>>, IValidate
{
    public string TypeName { get; set; } = default!;
}
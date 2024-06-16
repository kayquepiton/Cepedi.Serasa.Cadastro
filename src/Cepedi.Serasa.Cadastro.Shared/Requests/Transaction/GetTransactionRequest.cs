using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction
{
    public class GetTransactionRequest : IRequest<Result<GetTransactionResponse>>
    {
        public int Id { get; set; }
    }
}

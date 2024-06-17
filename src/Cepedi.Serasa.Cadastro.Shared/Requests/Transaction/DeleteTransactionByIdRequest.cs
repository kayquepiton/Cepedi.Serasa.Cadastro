using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction
{
    public class DeleteTransactionByIdRequest : IRequest<Result<DeleteTransactionByIdResponse>>
    {
        public int Id { get; set; }
    }
}

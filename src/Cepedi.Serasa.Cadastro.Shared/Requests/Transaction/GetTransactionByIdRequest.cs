using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction
{
    public class GetTransactionByIdRequest : IRequest<Result<GetTransactionByIdResponse>>
    {
        public int Id { get; set; }
    }
}

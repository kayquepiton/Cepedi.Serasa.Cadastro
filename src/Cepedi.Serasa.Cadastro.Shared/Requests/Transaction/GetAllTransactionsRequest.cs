using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction
{
    public class GetAllTransactionsRequest : IRequest<Result<List<GetAllTransactionsResponse>>>
    {
    }
}

using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Query
{
    public class DeleteQueryByIdRequest : IRequest<Result<DeleteQueryByIdResponse>>
    {
        public int Id { get; set; }
    }
}

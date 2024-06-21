using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.User.Requests
{
    public class GetUserByIdRequest : IRequest<Result<GetUserByIdResponse>>
    {
        public int Id { get; set; }
    }
}

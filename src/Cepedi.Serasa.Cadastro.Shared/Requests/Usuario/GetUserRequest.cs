using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.User.Requests
{
    public class GetUserRequest : IRequest<Result<GetUserResponse>>
    {
        public int Id { get; set; }
    }
}

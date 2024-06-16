using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.User.Requests
{
    public class DeleteUserRequest : IRequest<Result<DeleteUserResponse>>
    {
        public int Id { get; set; }
    }
}

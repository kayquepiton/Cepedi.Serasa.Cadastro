using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.User.Requests
{
    public class CreateUserRequest : IRequest<Result<CreateUserResponse>>
    {
        public required string Name { get; set; } = default!;
        public required string Username { get; set; } = default!;
        public required string Password { get; set; } = default!;
    }
}

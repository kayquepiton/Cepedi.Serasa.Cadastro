using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.Auth.Requests
{
    public class AuthenticateUserRequest : IRequest<Result<AuthenticateUserResponse>>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

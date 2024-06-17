using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.Auth.Requests
{
    public class AuthenticateUserRequest : IRequest<Result<AuthenticateUserResponse>>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}

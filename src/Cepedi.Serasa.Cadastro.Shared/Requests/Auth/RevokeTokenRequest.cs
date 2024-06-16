using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Auth.Requests
{
    public class RevokeTokenRequest : IRequest<Result>
    {
        public string RefreshToken { get; }
    }
}

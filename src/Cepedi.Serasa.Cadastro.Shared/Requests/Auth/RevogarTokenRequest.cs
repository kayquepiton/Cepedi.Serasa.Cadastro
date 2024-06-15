using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Auth.Requests
{
    public class RevogarTokenRequest : IRequest<Result>
    {
        public string RefreshToken { get; }

        public RevogarTokenRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}

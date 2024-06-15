using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Auth.Requests
{
    public class AutenticarUsuarioRequest : IRequest<Result<AutenticarUsuarioResponse>>
    {
        public string? Login { get; set; }
        public string? Senha { get; set; }
    }
}

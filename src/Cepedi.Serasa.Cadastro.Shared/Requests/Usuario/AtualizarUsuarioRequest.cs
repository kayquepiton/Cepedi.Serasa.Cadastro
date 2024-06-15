using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Usuario.Requests
{
    public class AtualizarUsuarioRequest : IRequest<Result<AtualizarUsuarioResponse>>
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
    }
}

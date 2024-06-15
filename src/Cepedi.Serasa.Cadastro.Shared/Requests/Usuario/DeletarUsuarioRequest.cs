using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Usuario.Requests
{
    public class DeletarUsuarioRequest : IRequest<Result<DeletarUsuarioResponse>>
    {
        public int Id { get; set; }
    }
}

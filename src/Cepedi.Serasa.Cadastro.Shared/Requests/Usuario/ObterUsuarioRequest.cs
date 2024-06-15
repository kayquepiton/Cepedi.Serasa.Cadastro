using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Usuario.Requests
{
    public class ObterUsuarioRequest : IRequest<Result<ObterUsuarioResponse>>
    {
        public int Id { get; set; }
    }
}

using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Usuario.Requests
{
    public class CriarUsuarioRequest : IRequest<Result<CriarUsuarioResponse>>
    {
        public string Nome { get; set; } = default!;
        public string Login { get; set; } = default!;
        public string Senha { get; set; } = default!; // Adicionado o campo de senha
    }
}

using Cepedi.Serasa.Cadastro.Shared.Responses.Pessoa;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Pessoa;
public class CriarPessoaRequest : IRequest<Result<CriarPessoaResponse>>, IValida
{
    public required string Nome { get; set; }
    public required string CPF { get; set; }
}

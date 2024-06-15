using Cepedi.Serasa.Cadastro.Shared.Responses.Pessoa;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Pessoa;
public class ObterPessoaPorIdRequest : IRequest<Result<ObterPessoaResponse>>
{
    public int Id { get; set; }
}

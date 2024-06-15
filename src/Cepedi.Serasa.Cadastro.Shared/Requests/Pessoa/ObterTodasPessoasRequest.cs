using Cepedi.Serasa.Cadastro.Shared.Responses.Pessoa;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Pessoa;
public class ObterTodasPessoasRequest : IRequest<Result<IEnumerable<ObterPessoaResponse>>>
{
}

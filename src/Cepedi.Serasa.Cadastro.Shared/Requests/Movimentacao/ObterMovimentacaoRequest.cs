using Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao;
public class ObterMovimentacaoRequest : IRequest<Result<ObterMovimentacaoResponse>>
{
    public int Id { get; set; }
}

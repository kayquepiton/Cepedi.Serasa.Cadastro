using Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao;
public class DeletarMovimentacaoRequest : IRequest<Result<DeletarMovimentacaoResponse>>
{
    public int Id { get; set; }
}
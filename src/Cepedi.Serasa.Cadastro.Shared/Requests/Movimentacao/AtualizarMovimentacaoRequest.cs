using Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao;

public class AtualizarMovimentacaoRequest : IRequest<Result<AtualizarMovimentacaoResponse>>, IValida
{
    public int Id { get; set; }
    public int IdTipoMovimentacao { get; set; }
    public DateTime DataHora { get; set; }
    public string NomeEstabelecimento { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}


using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao;

public class CriarMovimentacaoRequest : IRequest<Result<CriarMovimentacaoResponse>>, IValida
{
    public int IdTipoMovimentacao { get; set; }
    public int IdPessoa { get; set; }
    public DateTime DataHora { get; set; }
    public string NomeEstabelecimento { get; set; } = default!;
    public decimal Valor { get; set; }
    
}


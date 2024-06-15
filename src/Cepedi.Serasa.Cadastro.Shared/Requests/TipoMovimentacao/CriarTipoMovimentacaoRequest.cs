using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
public class CriarTipoMovimentacaoRequest : IRequest<Result<CriarTipoMovimentacaoResponse>>, IValida
{
    public string NomeTipo { get; set; } = default!;
}
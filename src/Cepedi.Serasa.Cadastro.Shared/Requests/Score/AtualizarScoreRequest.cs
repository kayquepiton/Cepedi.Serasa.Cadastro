using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score;
public class AtualizarScoreRequest : IRequest<Result<AtualizarScoreResponse>>, IValida
{
    public int Id { get; set; }
    public double Score { get; set; }
}

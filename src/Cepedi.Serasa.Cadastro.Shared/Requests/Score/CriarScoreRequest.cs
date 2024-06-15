using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score;
public class CriarScoreRequest : IRequest<Result<CriarScoreResponse>>, IValida
{
    public int IdPessoa { get; set; }
    public double Score { get; set; }
}
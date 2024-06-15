using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score;
public class ObterScoreRequest : IRequest<Result<ObterScoreResponse>>
{
    public int Id { get; set; }
}
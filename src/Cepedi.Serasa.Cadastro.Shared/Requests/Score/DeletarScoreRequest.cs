using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score;
public class DeletarScoreRequest : IRequest<Result<DeletarScoreResponse>>
{
    public int Id { get; set; }
}

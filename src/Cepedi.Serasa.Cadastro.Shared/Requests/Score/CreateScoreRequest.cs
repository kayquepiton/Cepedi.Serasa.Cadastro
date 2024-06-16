using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score
{
    public class CreateScoreRequest : IRequest<Result<CreateScoreResponse>>, IValidate
    {
        public int IdPerson { get; set; }
        public double Score { get; set; }
    }
}

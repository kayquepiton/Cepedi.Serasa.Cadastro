using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score
{
    public class UpdateScoreRequest : IRequest<Result<UpdateScoreResponse>>, IValidate
    {
        public required int Id { get; set; }
        public required double Score { get; set; }
    }
}

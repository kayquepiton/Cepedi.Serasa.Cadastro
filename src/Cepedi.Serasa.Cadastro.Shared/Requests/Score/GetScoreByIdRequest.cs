using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Score
{
    public class GetScoreByIdRequest : IRequest<Result<GetScoreByIdResponse>>
    {
        public int Id { get; set; }
    }
}

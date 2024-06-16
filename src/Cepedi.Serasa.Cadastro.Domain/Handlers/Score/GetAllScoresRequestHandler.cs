using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Score
{
    public class GetAllScoresRequestHandler : IRequestHandler<GetAllScoresRequest, Result<List<GetAllScoresResponse>>>
    {
        private readonly ILogger<GetAllScoresRequestHandler> _logger;
        private readonly IScoreRepository _scoreRepository;

        public GetAllScoresRequestHandler(ILogger<GetAllScoresRequestHandler> logger, IScoreRepository scoreRepository)
        {
            _logger = logger;
            _scoreRepository = scoreRepository;
        }

        public async Task<Result<List<GetAllScoresResponse>>> Handle(GetAllScoresRequest request, CancellationToken cancellationToken)
        {
            var scores = await _scoreRepository.GetAllScoresAsync();

            if (scores is null)
            {
                return Result.Error<List<GetAllScoresResponse>>(new Shared.Exceptions.AppException(RegistrationErrors.EmptyScoresList));
            }

            var response = new List<GetAllScoresResponse>();
            foreach (var score in scores)
            {
                response.Add(new GetAllScoresResponse(score.Id, score.IdPerson, score.Score));
            }

            return Result.Success(response);
        }
    }
}

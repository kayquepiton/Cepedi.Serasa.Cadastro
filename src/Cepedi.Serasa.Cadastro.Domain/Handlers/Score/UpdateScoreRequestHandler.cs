using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using System.Threading;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Score
{
    public class UpdateScoreRequestHandler :
        IRequestHandler<UpdateScoreRequest, Result<UpdateScoreResponse>>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly ILogger<UpdateScoreRequestHandler> _logger;

        public UpdateScoreRequestHandler(IScoreRepository scoreRepository, ILogger<UpdateScoreRequestHandler> logger)
        {
            _scoreRepository = scoreRepository;
            _logger = logger;
        }

        public async Task<Result<UpdateScoreResponse>> Handle(UpdateScoreRequest request, CancellationToken cancellationToken)
        {
            var scoreEntity = await _scoreRepository.GetScoreAsync(request.Id);

            if (scoreEntity is null)
            {
                return Result.Error<UpdateScoreResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalScoreIdId));
            }

            scoreEntity.Score = request.Score;

            await _scoreRepository.UpdateScoreAsync(scoreEntity);

            return Result.Success(new UpdateScoreResponse(scoreEntity.Id, scoreEntity.Score));
        }
    }
}

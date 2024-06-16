using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Score
{
    public class DeleteScoreRequestHandler : IRequestHandler<DeleteScoreRequest, Result<DeleteScoreResponse>>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly ILogger<DeleteScoreRequestHandler> _logger;

        public DeleteScoreRequestHandler(IScoreRepository scoreRepository, ILogger<DeleteScoreRequestHandler> logger)
        {
            _scoreRepository = scoreRepository;
            _logger = logger;
        }

        public async Task<Result<DeleteScoreResponse>> Handle(DeleteScoreRequest request, CancellationToken cancellationToken)
        {
            var scoreEntity = await _scoreRepository.GetScoreAsync(request.Id);

            if (scoreEntity is null)
            {
                return Result.Error<DeleteScoreResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidScoreId));
            }

            await _scoreRepository.DeleteScoreAsync(scoreEntity.Id);

            return Result.Success(new DeleteScoreResponse(scoreEntity.Id, scoreEntity.IdPerson, scoreEntity.Score));
        }
    }
}

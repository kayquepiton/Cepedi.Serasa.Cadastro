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
    public class DeleteScoreByIdRequestHandler : IRequestHandler<DeleteScoreByIdRequest, Result<DeleteScoreByIdResponse>>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly ILogger<DeleteScoreByIdRequestHandler> _logger;

        public DeleteScoreByIdRequestHandler(IScoreRepository scoreRepository, ILogger<DeleteScoreByIdRequestHandler> logger)
        {
            _scoreRepository = scoreRepository;
            _logger = logger;
        }

        public async Task<Result<DeleteScoreByIdResponse>> Handle(DeleteScoreByIdRequest request, CancellationToken cancellationToken)
        {
            var scoreEntity = await _scoreRepository.GetScoreAsync(request.Id);

            if (scoreEntity is null)
            {
                return Result.Error<DeleteScoreByIdResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalScoreIdId));
            }

            await _scoreRepository.DeleteScoreAsync(scoreEntity.Id);

            return Result.Success(new DeleteScoreByIdResponse(scoreEntity.Id, scoreEntity.PersonId, scoreEntity.Score));
        }
    }
}

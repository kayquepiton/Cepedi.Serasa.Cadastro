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
    public class GetScoreRequestHandler : IRequestHandler<GetScoreRequest, Result<GetScoreResponse>>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly ILogger<GetScoreRequestHandler> _logger;

        public GetScoreRequestHandler(IScoreRepository scoreRepository, ILogger<GetScoreRequestHandler> logger)
        {
            _scoreRepository = scoreRepository;
            _logger = logger;
        }

        public async Task<Result<GetScoreResponse>> Handle(GetScoreRequest request, CancellationToken cancellationToken)
        {
            var scoreEntity = await _scoreRepository.GetScoreAsync(request.Id);

            if (scoreEntity is null)
            {
                return Result.Error<GetScoreResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidScoreId));
            }

            return Result.Success(new GetScoreResponse(scoreEntity.Id, scoreEntity.IdPerson, scoreEntity.Score));
        }
    }
}

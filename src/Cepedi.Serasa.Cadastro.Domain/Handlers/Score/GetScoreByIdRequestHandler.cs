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
    public class GetScoreByIdRequestHandler : IRequestHandler<GetScoreByIdRequest, Result<GetScoreByIdResponse>>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly ILogger<GetScoreByIdRequestHandler> _logger;

        public GetScoreByIdRequestHandler(IScoreRepository scoreRepository, ILogger<GetScoreByIdRequestHandler> logger)
        {
            _scoreRepository = scoreRepository;
            _logger = logger;
        }

        public async Task<Result<GetScoreByIdResponse>> Handle(GetScoreByIdRequest request, CancellationToken cancellationToken)
        {
            var scoreEntity = await _scoreRepository.GetScoreAsync(request.Id);

            if (scoreEntity is null)
            {
                return Result.Error<GetScoreByIdResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalScoreIdId));
            }

            return Result.Success(new GetScoreByIdResponse(scoreEntity.Id, scoreEntity.PersonId, scoreEntity.Score));
        }
    }
}

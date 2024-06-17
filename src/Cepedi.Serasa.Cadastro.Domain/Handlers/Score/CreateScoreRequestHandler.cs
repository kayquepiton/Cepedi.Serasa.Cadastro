using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Score
{
    public class CreateScoreRequestHandler : IRequestHandler<CreateScoreRequest, Result<CreateScoreResponse>>
    {
        private readonly ILogger<CreateScoreRequestHandler> _logger;
        private readonly IScoreRepository _scoreRepository;
        private readonly IPersonRepository _personRepository;

        public CreateScoreRequestHandler(IScoreRepository scoreRepository, ILogger<CreateScoreRequestHandler> logger, IPersonRepository personRepository)
        {
            _scoreRepository = scoreRepository;
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<Result<CreateScoreResponse>> Handle(CreateScoreRequest request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonAsync(request.PersonId);
            if (person is null)
            {
                return Result.Error<CreateScoreResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalidPersonId));
            }

            var existingScore = await _scoreRepository.GetPersonScoreAsync(request.PersonId);
            if (existingScore != null)
            {
                return Result.Error<CreateScoreResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.ScoreAlreadyExists));
            }

            var score = new ScoreEntity()
            {
                Score = request.Score,
                PersonId = request.PersonId,
            };

            await _scoreRepository.CreateScoreAsync(score);

            return Result.Success(new CreateScoreResponse(score.Id, score.PersonId, score.Score));
        }
    }
}

using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Enums;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
public class AtualizarScoreRequestHandler :
    IRequestHandler<AtualizarScoreRequest, Result<AtualizarScoreResponse>>
{
    private readonly IScoreRepository _scoreRepository;
    private readonly ILogger<AtualizarScoreRequestHandler> _logger;

    public AtualizarScoreRequestHandler(IScoreRepository scoreRepository, ILogger<AtualizarScoreRequestHandler> logger)
    {
        _scoreRepository = scoreRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarScoreResponse>> Handle(AtualizarScoreRequest request, CancellationToken cancellationToken)
    {
        var scoreEntity = await _scoreRepository.ObterScoreAsync(request.Id);

        if (scoreEntity == null)
        {
            return Result.Error<AtualizarScoreResponse>(new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdScoreInvalido));
        }

        scoreEntity.Score = request.Score;

        await _scoreRepository.AtualizarScoreAsync(scoreEntity);

        return Result.Success(new AtualizarScoreResponse(scoreEntity.Id, scoreEntity.Score));
    }
}

using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Enums;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
public class DeletarScoreRequestHandler :
    IRequestHandler<DeletarScoreRequest, Result<DeletarScoreResponse>>
{
    private readonly IScoreRepository _scoreRepository;
    private readonly ILogger<DeletarScoreRequestHandler> _logger;

    public DeletarScoreRequestHandler(IScoreRepository scoreRepository, ILogger<DeletarScoreRequestHandler> logger)
    {
        _scoreRepository = scoreRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarScoreResponse>> Handle(DeletarScoreRequest request, CancellationToken cancellationToken)
    {
        var scoreEntity = await _scoreRepository.ObterScoreAsync(request.Id);

        if (scoreEntity == null)
        {
            return Result.Error<DeletarScoreResponse>(new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdScoreInvalido));
        }

        await _scoreRepository.DeletarScoreAsync(scoreEntity.Id);

        return Result.Success(new DeletarScoreResponse(scoreEntity.Id, scoreEntity.IdPessoa, scoreEntity.Score));
    }
}

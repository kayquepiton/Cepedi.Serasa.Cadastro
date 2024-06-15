﻿using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
public class CriarScoreRequestHandler
    : IRequestHandler<CriarScoreRequest, Result<CriarScoreResponse>>
{
    private readonly ILogger<CriarScoreRequestHandler> _logger;
    private readonly IScoreRepository _scoreRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public CriarScoreRequestHandler(IScoreRepository scoreRepository, ILogger<CriarScoreRequestHandler> logger, IPessoaRepository pessoaRepository)
    {
        _scoreRepository = scoreRepository;
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<CriarScoreResponse>> Handle(CriarScoreRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await _pessoaRepository.ObterPessoaAsync(request.IdPessoa);
        if (pessoa == null)
        {
            return Result.Error<CriarScoreResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdPessoaInvalido));
        }

        // Verifica se a pessoa já possui um score
        var scoreExistente = await _scoreRepository.ObterPessoaScoreAsync(request.IdPessoa);
        if (scoreExistente != null)
        {
            // Se já existe um score para essa pessoa, retorna um erro ou uma mensagem indicando isso
            return Result.Error<CriarScoreResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.ScoreJaExistente));
        }

        var score = new ScoreEntity()
        {
            Score = request.Score,
            IdPessoa = request.IdPessoa,
        };

        await _scoreRepository.CriarScoreAsync(score);

        return Result.Success(new CriarScoreResponse(score.Id, score.IdPessoa, score.Score));
    }

}

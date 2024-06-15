using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository;

public interface IScoreRepository
{
    Task<ScoreEntity> CriarScoreAsync(ScoreEntity score);
    Task<ScoreEntity> ObterScoreAsync(int id);
    Task<List<ScoreEntity>> ObterTodosScoresAsync();
    Task<ScoreEntity> AtualizarScoreAsync(ScoreEntity score);
    Task<ScoreEntity> ObterPessoaScoreAsync(int id);
    Task<ScoreEntity> DeletarScoreAsync(int id);
}

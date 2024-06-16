using System.Collections.Generic;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository
{
    public interface IScoreRepository
    {
        Task<ScoreEntity> CreateScoreAsync(ScoreEntity score);
        Task<ScoreEntity> GetScoreAsync(int id);
        Task<List<ScoreEntity>> GetAllScoresAsync();
        Task<ScoreEntity> UpdateScoreAsync(ScoreEntity score);
        Task<ScoreEntity> GetPersonScoreAsync(int id);
        Task<ScoreEntity> DeleteScoreAsync(int id);
    }
}

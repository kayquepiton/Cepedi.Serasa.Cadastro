using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly ApplicationDbContext _context;

        public ScoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScoreEntity> UpdateScoreAsync(ScoreEntity score)
        {
            _context.Score.Update(score);
            await _context.SaveChangesAsync();
            return score;
        }

        public async Task<ScoreEntity> CreateScoreAsync(ScoreEntity score)
        {
            _context.Score.Add(score);
            await _context.SaveChangesAsync();
            return score;
        }

        public async Task<List<ScoreEntity>> GetAllScoresAsync()
        {
            return await _context.Score.ToListAsync();
        }

        public async Task<ScoreEntity> GetScoreAsync(int id)
        {
            return await _context.Score.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ScoreEntity> GetPersonScoreAsync(int id)
        {
            return await _context.Score.FirstOrDefaultAsync(e => e.IdPerson == id);
        }

        public async Task<ScoreEntity> DeleteScoreAsync(int id)
        {
            var scoreEntity = await GetScoreAsync(id);
            if (scoreEntity is null)
            {
                return null;
            }
            _context.Score.Remove(scoreEntity);
            await _context.SaveChangesAsync();
            return scoreEntity;
        }
    }
}

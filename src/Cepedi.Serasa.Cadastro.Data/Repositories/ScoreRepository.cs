﻿using Cepedi.Serasa.Cadastro.Domain.Entities;
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

        public async Task<ScoreEntity> AtualizarScoreAsync(ScoreEntity score)
        {
            _context.Score.Update(score);

            await _context.SaveChangesAsync();

            return score;
        }

        public async Task<ScoreEntity> CriarScoreAsync(ScoreEntity score)
        {
            _context.Score.Add(score);

            await _context.SaveChangesAsync();

            return score;
        }

        public async Task<List<ScoreEntity>> GetScoresAsync()
        {
            return await _context.Score.ToListAsync();
        }

        public async Task<ScoreEntity> ObterScoreAsync(int id)
        {
            return await
                _context.Score.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ScoreEntity>> ObterTodosScoresAsync()
        {
            return await _context.Set<ScoreEntity>().ToListAsync();
        }

        public async Task<ScoreEntity> ObterPessoaScoreAsync(int id)
        {
            return await
                _context.Score.Where(e => e.IdPessoa == id).FirstOrDefaultAsync();
        }

        public async Task<ScoreEntity> DeletarScoreAsync(int id)
        {
            var scoreEntity = await ObterScoreAsync(id);

            if (scoreEntity == null)
            {
                return null;
            }

            _context.Score.Remove(scoreEntity);

            await _context.SaveChangesAsync();

            return scoreEntity;
        }
    }
}

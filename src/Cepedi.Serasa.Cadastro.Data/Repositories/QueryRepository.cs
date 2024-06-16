using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Data.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public QueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<QueryEntity> UpdateQueryAsync(QueryEntity query)
        {
            _context.Query.Update(query);
            await _context.SaveChangesAsync();
            return query;
        }

        public async Task<QueryEntity> CreateQueryAsync(QueryEntity query)
        {
            _context.Query.Add(query);
            await _context.SaveChangesAsync();
            return query;
        }

        public async Task<QueryEntity> GetQueryAsync(int id)
        {
            return await _context.Query
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<QueryEntity>> GetAllQueriesAsync()
        {
            return await _context.Query.ToListAsync();
        }

        public async Task<PersonEntity> GetPersonForQueryAsync(int id)
        {
            return await _context.Person
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<QueryEntity> DeleteQueryAsync(int id)
        {
            var query = await GetQueryAsync(id);
            if (query == null)
            {
                return null;
            }

            _context.Query.Remove(query);
            await _context.SaveChangesAsync();
            return query;
        }
    }
}

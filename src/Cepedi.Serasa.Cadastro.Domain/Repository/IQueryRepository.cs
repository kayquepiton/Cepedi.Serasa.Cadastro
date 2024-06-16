using Cepedi.Serasa.Cadastro.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Repository
{
    public interface IQueryRepository
    {
        Task<QueryEntity> CreateQueryAsync(QueryEntity query);
        Task<QueryEntity> GetQueryAsync(int id);
        Task<List<QueryEntity>> GetAllQueriesAsync();
        Task<QueryEntity> UpdateQueryAsync(QueryEntity query);
        Task<PersonEntity> GetPersonForQueryAsync(int id);
        Task<QueryEntity> DeleteQueryAsync(int id);
    }
}

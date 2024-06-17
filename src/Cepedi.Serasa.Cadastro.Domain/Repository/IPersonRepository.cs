using Cepedi.Serasa.Cadastro.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Repository
{
    public interface IPersonRepository
    {
        Task<PersonEntity> GetPersonAsync(int id);
        Task<List<PersonEntity>> GetAllPersonsAsync();
        Task<PersonEntity> CreatePersonAsync(PersonEntity person);
        Task<PersonEntity> UpdatePersonAsync(PersonEntity person);
        Task<PersonEntity> DeletePersonAsync(int id);
    }
}

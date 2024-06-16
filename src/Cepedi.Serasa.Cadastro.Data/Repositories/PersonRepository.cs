using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PersonEntity> UpdatePersonAsync(PersonEntity person)
        {
            _context.Person.Update(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<PersonEntity> CreatePersonAsync(PersonEntity person)
        {
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<PersonEntity?> DeletePersonAsync(int id)
        {
            var person = await GetPersonAsync(id);
            if (person == null) return null;

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<PersonEntity> GetPersonAsync(int id)
        {
            return await _context.Person.FirstOrDefaultAsync(person => person.Id == id);
        }

        public async Task<List<PersonEntity>> GetPersonsAsync()
        {
            return await _context.Person.ToListAsync();
        }
    }
}

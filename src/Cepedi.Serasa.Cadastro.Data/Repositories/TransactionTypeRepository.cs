using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionTypeEntity> CreateTransactionTypeAsync(TransactionTypeEntity transactionType)
        {
            _context.TransactionType.Add(transactionType);
            await _context.SaveChangesAsync();
            return transactionType;
        }

        public async Task<TransactionTypeEntity> GetTransactionTypeAsync(int id)
        {
            return await _context.TransactionType.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<TransactionTypeEntity>> GetAllTransactionTypesAsync()
        {
            return await _context.TransactionType.ToListAsync();
        }

        public async Task<TransactionTypeEntity> UpdateTransactionTypeAsync(TransactionTypeEntity transactionType)
        {
            _context.TransactionType.Update(transactionType);
            await _context.SaveChangesAsync();
            return transactionType;
        }

        public async Task<TransactionTypeEntity?> DeleteTransactionTypeAsync(int id)
        {
            var transactionType = await GetTransactionTypeAsync(id);
            if (transactionType == null)
                return null;

            _context.TransactionType.Remove(transactionType);
            await _context.SaveChangesAsync();
            return transactionType;
        }
    }
}

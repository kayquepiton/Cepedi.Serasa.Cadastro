using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionEntity> CreateTransactionAsync(TransactionEntity transaction)
        {
            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<TransactionEntity> GetTransactionAsync(int id)
        {
            return await _context.Transaction.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<TransactionEntity>> GetAllTransactionsAsync()
        {
            return await _context.Transaction.ToListAsync();
        }

        public async Task<TransactionEntity> UpdateTransactionAsync(TransactionEntity transaction)
        {
            _context.Transaction.Update(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<TransactionEntity> DeleteTransactionAsync(int id)
        {
            var transaction = await GetTransactionAsync(id);
            if (transaction is null)
                return null;

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}

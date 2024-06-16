using Cepedi.Serasa.Cadastro.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Repository
{
    public interface ITransactionRepository
    {
        Task<TransactionEntity> CreateTransactionAsync(TransactionEntity transaction);
        Task<TransactionEntity> GetTransactionAsync(int id);
        Task<List<TransactionEntity>> GetAllTransactionsAsync();
        Task<TransactionEntity> UpdateTransactionAsync(TransactionEntity transaction);
        Task<TransactionEntity> DeleteTransactionAsync(int id);
    }
}

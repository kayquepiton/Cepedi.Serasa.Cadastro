﻿using Cepedi.Serasa.Cadastro.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Repository
{
    public interface ITransactionTypeRepository
    {
        Task<TransactionTypeEntity> CreateTransactionTypeAsync(TransactionTypeEntity transactionType);
        Task<TransactionTypeEntity> GetTransactionTypeAsync(int id);
        Task<List<TransactionTypeEntity>> GetAllTransactionTypesAsync();
        Task<TransactionTypeEntity> UpdateTransactionTypeAsync(TransactionTypeEntity transactionType);
        Task<TransactionTypeEntity> DeleteTransactionTypeAsync(int id);
    }
}

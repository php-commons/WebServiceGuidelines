using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain.Entities;

namespace PHP.WebServiceConcept.Domain
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByTransactionIdAsync(string transactionId);
        Task<bool> AddNewTransactionAsync(Transaction newTransaction);
        Task<int> SaveChangesAsync();
    }
}

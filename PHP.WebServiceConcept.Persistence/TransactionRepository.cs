using System.Collections.Concurrent;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain;
using PHP.WebServiceConcept.Domain.Entities;

namespace PHP.WebServiceConcept.Persistence
{
    public class TransactionRepository : ITransactionRepository
    {
        private static ConcurrentDictionary<string, Transaction> _accountTransactions =
            new ConcurrentDictionary<string, Transaction>();

        public async Task<Transaction> GetByTransactionIdAsync(string transactionId)
        {
            Transaction transaction;
            var found = _accountTransactions.TryGetValue(transactionId, out transaction);

            if (found)
                return transaction;

            return null;
        }

        public async Task<bool> AddNewTransactionAsync(Transaction newTransaction)
        {
            return _accountTransactions.TryAdd(newTransaction.TransactionId, newTransaction);
        }

        public async Task<int> SaveChangesAsync()
        {
            return 0;
        }
    }
}
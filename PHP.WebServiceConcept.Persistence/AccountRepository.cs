using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain;
using PHP.WebServiceConcept.Domain.Entities;

namespace PHP.WebServiceConcept.Persistence
{
    public class AccountRepository : IAccountRepository
    {
        private static ConcurrentDictionary<string, Account> _accounts = 
            new ConcurrentDictionary<string, Account>();

        static AccountRepository()
        {
            var newAccount = new Account("test","Test Account",0.0m);
            _accounts.TryAdd("test", newAccount);
        }

        public async Task<bool> AccountExistsAsync(string accountId)
        {
            return _accounts.ContainsKey(accountId);
        }

        public async Task<Account> GetAccountAsync(string accountId)
        {
            Account account;
            if (_accounts.TryGetValue(accountId, out account) == false)
            {
                return null;
            }

            return account;
        }

        public async Task<bool> AddNewAccountAsync(Account newAccount)
        {
            return _accounts.TryAdd(newAccount.AccountId, newAccount);
        }

        public async Task<int> SaveChangesAsync()
        {
            return 0;
        }
    }
}

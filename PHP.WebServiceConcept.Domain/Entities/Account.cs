using System;

namespace PHP.WebServiceConcept.Domain.Entities
{
    public class Account
    {
        // We use private backing fields and public properties
        // to support Entity Framework Core.

        private string _accountId;
        private string _name;
        private decimal _currentBalance;

        public string AccountId => _accountId;

        public string Name => _name;
       
        public decimal CurrentBalance => _currentBalance;
        
        public Account()
        {
            
        }

        public Account(string accountId, string name, decimal startingBalance)
        {
            if (String.IsNullOrEmpty(accountId))
                throw new ArgumentException("accountId can not be null or empty",nameof(accountId));
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name can not be null or empty", nameof(name));
            if (startingBalance < 0.0m)
                throw new ArgumentOutOfRangeException(nameof(startingBalance), startingBalance, "startingBalance must be zero or greater");

            _accountId = accountId;
            _currentBalance = startingBalance;
            _name = name;
        }

        public void AddToBalance(decimal amount)
        {
            _currentBalance += amount;
        }

        public void SubtractFromBalance(decimal amount)
        {
            _currentBalance -= amount;
        }
    }
}

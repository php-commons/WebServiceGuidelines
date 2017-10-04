using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Entities
{
    public class Transaction
    {
        public const string DepositTransactionType = "Deposit";
        public const string WithdrawalTransactionType = "Withdrawal";

        // Having private this helps with persistence in EF Core. 
        // HINT: But, is that a good thing?
        private Account _account;
        private string _transactionType;
        private string _transactionId;
        private DateTime _sourceDateTime;
        private DateTime? _processedOnDateTime;
        private decimal _transactionAmount;

        public Account Account => _account;

        public string TransactionType => _transactionType;
        public string TransactionId => _transactionId;
        public DateTime SourceDateTime => _sourceDateTime;
        public DateTime? ProcessedOnDateTime => _processedOnDateTime;
        public decimal TransactionAmount => _transactionAmount;
        
        public Transaction(Account account, string transactionType, decimal amount)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            if (String.IsNullOrEmpty(transactionType))
                throw new ArgumentException("transactionType can not be null or empty", nameof(transactionType));
            if (amount <= 0.0m)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "amount must be zero or greater");

            _account = account;
            _sourceDateTime = DateTime.UtcNow;
            _processedOnDateTime = null;

            _transactionAmount = amount;
            _transactionId = Guid.NewGuid().ToString("N");
            _transactionType = transactionType;
        }

        public void Process(DateTime processedOnDateTime)
        {
            if (processedOnDateTime < _sourceDateTime)
                throw new ArgumentException("Transactions must be processed after the source date");

            switch (_transactionType)
            {
                case DepositTransactionType:
                    _account.AddToBalance(_transactionAmount);
                    break;
                case WithdrawalTransactionType:
                    _account.SubtractFromBalance(_transactionAmount);
                    break;
                default:
                    // Do nothing
                    break;
            }

            _processedOnDateTime = processedOnDateTime;
        }
    }
}

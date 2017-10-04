using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Commands
{
    public class ProcessTransactionCommand
    {
        public string AccountId { get; }
        public string TransactionId { get; }

        public ProcessTransactionCommand(string accountId, string transactionId)
        {
            if (String.IsNullOrEmpty(accountId))
                throw new ArgumentException("accountId must be a non-empty string", nameof(accountId));
            if (String.IsNullOrEmpty(transactionId))
                throw new ArgumentException("transactionId must be a non-empty string", nameof(transactionId));

            AccountId = accountId;
            TransactionId = transactionId;
        }
    }
}

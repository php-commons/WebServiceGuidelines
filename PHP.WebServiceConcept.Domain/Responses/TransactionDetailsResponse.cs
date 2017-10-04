using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Responses
{
    public class TransactionDetailsResponse
    {
        public string AccountId { get; }
        public decimal Amount { get; }
        public string TransactionId { get; }
        public string TransactionType { get; }
        public bool WasProcessed { get; }

        public TransactionDetailsResponse(
            string accountId,
            decimal amount,
            string transactionId,
            string transactionType,
            bool wasProcessed)
        {
            AccountId = accountId;
            Amount = amount;
            TransactionId = transactionId;
            TransactionType = transactionType;
            WasProcessed = wasProcessed;
        }
    }
}

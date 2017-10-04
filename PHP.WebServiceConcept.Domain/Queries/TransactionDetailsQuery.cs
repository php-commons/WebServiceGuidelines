using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Queries
{
    public class TransactionDetailsQuery
    {
        public string TransactionId { get; }
        public TransactionDetailsQuery(string transactionId)
        {
            TransactionId = transactionId;
        }
    }
}

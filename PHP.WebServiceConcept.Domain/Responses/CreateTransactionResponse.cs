using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Responses
{
    public class CreateTransactionResponse
    {
        public string TransactionId { get; }

        public CreateTransactionResponse(string transactionId)
        {
            if (String.IsNullOrEmpty(transactionId))
                throw new ArgumentNullException(nameof(transactionId));

            TransactionId = transactionId;
        }
    }
}

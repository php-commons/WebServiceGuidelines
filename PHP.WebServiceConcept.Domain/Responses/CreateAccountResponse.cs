using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Responses
{
    public class CreateAccountResponse
    {
        public string AccountId { get; }

        public CreateAccountResponse(string accountId)
        {
            if (String.IsNullOrEmpty(accountId))
                throw new ArgumentNullException(nameof(accountId));

            AccountId = accountId;
        }
    }
}

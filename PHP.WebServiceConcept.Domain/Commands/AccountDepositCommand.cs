using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Commands
{
    public class AccountDepositCommand
    {
        public string AccountId { get; }
        public decimal Amount { get; set; }
        public string SourceId { get; }

        public AccountDepositCommand(string accountId, string sourceId)
        {
            if (String.IsNullOrEmpty(accountId))
                throw new ArgumentException("accountId must be a non-empty string", nameof(accountId));
            if (String.IsNullOrEmpty(sourceId))
                throw new ArgumentException("sourceId must be a non-empty string", nameof(sourceId));

            AccountId = accountId;
            SourceId = sourceId;
        }
    }
}

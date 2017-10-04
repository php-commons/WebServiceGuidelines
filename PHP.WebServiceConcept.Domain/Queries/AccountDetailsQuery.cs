namespace PHP.WebServiceConcept.Domain.Queries
{
    public class AccountDetailsQuery
    {
        public string AccountId { get; }
        public string SourceId { get;  }

        public AccountDetailsQuery(string accountId, string sourceId)
        {
            AccountId = accountId;
            SourceId = sourceId;
        }
    }
}

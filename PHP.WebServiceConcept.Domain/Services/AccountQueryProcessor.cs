using System;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain.Queries;
using PHP.WebServiceConcept.Domain.Responses;

namespace PHP.WebServiceConcept.Domain.Services
{
    internal class AccountQueryProcessor : IAccountQueryProcessor
    {
        private readonly IAccountRepository _accountRepository;

        public AccountQueryProcessor(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountDetailsResponse> ExecuteQueryAsync(AccountDetailsQuery query)
        {
            if (query==null)
                throw new ArgumentNullException(nameof(query));

            var account = await _accountRepository.GetAccountAsync(query.AccountId);
            if (account == null)
                throw new QueryProcessingException(String.Format("No account with id {0} was found.",
                    query.AccountId));

            return new AccountDetailsResponse
            {
                Balance = account.CurrentBalance,
                Name = account.Name
            };
        }
    }
}

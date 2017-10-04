using System;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain.Commands;
using PHP.WebServiceConcept.Domain.Entities;
using PHP.WebServiceConcept.Domain.Responses;

namespace PHP.WebServiceConcept.Domain.Services
{
    internal class AccountCommandProcessor : IAccountCommandProcessor
    {
        private static readonly Random RandomNumberGenerator = new Random();

        private readonly IAccountRepository _accountRepository;

        public AccountCommandProcessor(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CreateAccountResponse> ExecuteCommandAsync(AccountCreateCommand command)
        {
            var newAccountId = CreateRandomAccountId();
            while (await _accountRepository.AccountExistsAsync(newAccountId) == false)
            {
                newAccountId = CreateRandomAccountId();
            }

            var newAccount = new Account(newAccountId, command.Name, command.StartingBalance);
            var created = await _accountRepository.AddNewAccountAsync(newAccount);

            if (created == false)
                return null;

            return new CreateAccountResponse(newAccountId);
        }

        private string CreateRandomAccountId()
        {
            return String.Format("FI-{0}", RandomNumberGenerator.Next(1000, 10000000).ToString("D10"));
        }
    }
}
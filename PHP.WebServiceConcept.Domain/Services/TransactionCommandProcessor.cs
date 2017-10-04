using System;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain.Commands;
using PHP.WebServiceConcept.Domain.Entities;
using PHP.WebServiceConcept.Domain.Responses;

namespace PHP.WebServiceConcept.Domain.Services
{
    internal class TransactionCommandProcessor : ITransactionCommandProcessor
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionCommandProcessor(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<CreateTransactionResponse> ExecuteCommandAsync(AccountDepositCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var account = await _accountRepository.GetAccountAsync(command.AccountId);
            if (account == null)
                throw new CommandProcessingException(
                    String.Format("No account with id {0} was found.",command.AccountId));

            var newTransaction =
                new Transaction(account, Transaction.DepositTransactionType, command.Amount);

            var saved = await _transactionRepository.AddNewTransactionAsync(newTransaction);
            if (saved == false)
                return null;

            return new CreateTransactionResponse(newTransaction.TransactionId);
        }

        public async Task<CreateTransactionResponse> ExecuteCommandAsync(AccountWithdrawalCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var account = await _accountRepository.GetAccountAsync(command.AccountId);
            if (account == null)
                throw new CommandProcessingException(String.Format("No account with id {0} was found.",
                    command.AccountId));

            var newTransaction =
                new Transaction(account, Transaction.WithdrawalTransactionType, command.Amount);

            var saved = await _transactionRepository.AddNewTransactionAsync(newTransaction);
            if (saved == false)
                return null;

            return new CreateTransactionResponse(newTransaction.TransactionId);
        }
    }
}

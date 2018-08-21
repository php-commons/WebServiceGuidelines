using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PHP.WebServiceConcept.Domain;
using PHP.WebServiceConcept.Domain.Commands;
using PHP.WebServiceConcept.Domain.Queries;

namespace PHP.WebServiceConcept.AccountWebService.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountQueryProcessor _queryProcessor;
        private readonly ITransactionCommandProcessor _commandProcessor;

        public AccountsController (IAccountQueryProcessor queryProcessor, ITransactionCommandProcessor commandProcessor)
        {            
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
        }

        // GET api/accounts/{accountId}
        /// <summary>
        /// Given an account, return the account information.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAsync(string accountId)
        {
            Contract.Requires(!String.IsNullOrEmpty(accountId));
            if (String.IsNullOrEmpty(accountId))
                return BadRequest("Empty or null accountId is not allowed");

            var accountDetailsQuery = new AccountDetailsQuery(accountId, "Web");
            var response = await _queryProcessor.ExecuteQueryAsync(accountDetailsQuery);

            if (response == null)
                return NotFound(accountId);

            return Ok(response);
        }

        // POST api/accounts/{accountid}/deposit
        /// <summary>
        /// Given an account, create a deposit.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> DepositAsync(string accountId, [FromBody] decimal amount)
        {
            Contract.Requires(!String.IsNullOrEmpty(accountId));
            Contract.Requires(amount > 0.0m);
            if (String.IsNullOrEmpty(accountId))
                return BadRequest("Empty or null accountId is not allowed");
            if (amount <= 0.0m)
                return BadRequest("amount must be greater than zero");

            var command = new AccountDepositCommand(accountId, "Web");
            command.Amount = amount;

            var response = await _commandProcessor.ExecuteCommandAsync(command);
            if (response == null)
            { 
                return new RetryAfterResult(0.5m);
            }

            return CreatedAtAction("GetTransactionDetailsAsync", "Transactions", new {transactionId = response.TransactionId}, response.TransactionId);
        }

        // POST api/accounts/{accountId}/withdrawal
        /// <summary>
        /// Given an account, create a withdrawal. 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost("{accountId}/withdrawal")]
        public async Task<IActionResult> WithdrawalAsync(string accountId, [FromBody] decimal amount)
        {
            if (String.IsNullOrEmpty(accountId))
                return BadRequest("Empty or null accountId is not allowed");
            if (amount <= 0.0m)
                return BadRequest("amount must be greater than zero");

            var command = new AccountWithdrawalCommand(accountId, "Web");
            command.Amount = amount;

            var response = await _commandProcessor.ExecuteCommandAsync(command);
            if (response == null)
                return new RetryAfterResult(0.5m);

            return CreatedAtAction("GetTransactionDetailsAsync", "Transactions", new {transactionId = response.TransactionId}, response.TransactionId);
        }
    }
}

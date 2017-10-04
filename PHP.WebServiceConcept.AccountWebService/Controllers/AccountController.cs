using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PHP.WebServiceConcept.Domain;
using PHP.WebServiceConcept.Domain.Commands;
using PHP.WebServiceConcept.Domain.Queries;

namespace PHP.WebServiceConcept.AccountWebService.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IAccountQueryProcessor _queryProcessor;
        private ITransactionCommandProcessor _commandProcessor;

        public AccountController (ITransactionCommandProcessor commandProcessor, IAccountQueryProcessor queryProcesor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcesor;
        }

        // GET api/account/accountId
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAsync(string accountId)
        {
            if (String.IsNullOrEmpty(accountId))
                return BadRequest("Empty or null accountId is not allowed");

            var accountDetailsQuery = new AccountDetailsQuery(accountId, "FAKE");
            var response = await _queryProcessor.ExecuteQueryAsync(accountDetailsQuery);

            if (response == null)
                return NotFound(accountId);

            return Ok(response);
        }

        // POST api/account/{accountid}/deposit
        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> DepositAsync(string accountId, [FromBody] decimal amount)
        {
            if (String.IsNullOrEmpty(accountId))
                return BadRequest("Empty or null accountId is not allowed");
            if (amount <= 0.0m)
                return BadRequest("amount must be greater than zero");

            var command = new AccountDepositCommand(accountId, "FAKE");
            command.Amount = amount;

            var response = await _commandProcessor.ExecuteCommandAsync(command);
            if (response == null)
            { 
                return new RetryAfterResult(0.5m);
            }

            return CreatedAtAction("GetAsync", "Account", new {accountId = accountId}, response.TransactionId);
        }

        // POST api/account/accountId/withdrawal
        [HttpPost("{accountId}/withdrawal")]
        public async Task<IActionResult> WithdrawalAsync(string accountId, [FromBody] decimal amount)
        {
            if (String.IsNullOrEmpty(accountId))
                return BadRequest("Empty or null accountId is not allowed");
            if (amount <= 0.0m)
                return BadRequest("amount must be greater than zero");

            var command = new AccountWithdrawalCommand(accountId, "FAKE");
            command.Amount = amount;

            var response = await _commandProcessor.ExecuteCommandAsync(command);
            if (response == null)
                return new RetryAfterResult(0.5m);

            return CreatedAtAction("GetAsync", "Account", new {accountId = accountId}, response.TransactionId);
        }
    }
}

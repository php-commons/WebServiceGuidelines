using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PHP.WebServiceConcept.Domain;
using PHP.WebServiceConcept.Domain.Queries;

namespace PHP.WebServiceConcept.AccountWebService.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionQueryProcessor _transactionQueryProcessor;

        public TransactionController(ITransactionQueryProcessor transactionQueryProcessor)
        {
            _transactionQueryProcessor = transactionQueryProcessor;
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult> GetTransactionDetailsAsync(string transactionId)
        {
            if (String.IsNullOrEmpty(transactionId))
                return BadRequest("transactionId can not be null or empty");

            var query = new TransactionDetailsQuery(transactionId);

            var response = await _transactionQueryProcessor.ExecuteQueryAsync(query);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
        
        // HINT: add command to process transaction there. 
    }
}

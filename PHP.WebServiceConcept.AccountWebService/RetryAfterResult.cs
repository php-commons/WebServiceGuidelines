using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace PHP.WebServiceConcept.AccountWebService
{
    public class RetryAfterResult : IActionResult
    {
        private readonly decimal _afterSeconds;

        public RetryAfterResult(decimal afterSeconds)
        {
            _afterSeconds = afterSeconds;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.Headers.Add("Reply-After", new StringValues(_afterSeconds.ToString("#.##")));
            response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        }
    }
}

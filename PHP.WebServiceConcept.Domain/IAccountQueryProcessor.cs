using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain.Queries;
using PHP.WebServiceConcept.Domain.Responses;

namespace PHP.WebServiceConcept.Domain
{
    public interface IAccountQueryProcessor
    {
        Task<AccountDetailsResponse> ExecuteQueryAsync(AccountDetailsQuery query);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain.Commands;
using PHP.WebServiceConcept.Domain.Responses;

namespace PHP.WebServiceConcept.Domain
{
    public interface ITransactionCommandProcessor
    {
        Task<CreateTransactionResponse> ExecuteCommandAsync(AccountDepositCommand command);
        Task<CreateTransactionResponse> ExecuteCommandAsync(AccountWithdrawalCommand command);
    }
}

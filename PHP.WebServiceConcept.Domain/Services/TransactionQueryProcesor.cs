using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain.Queries;
using PHP.WebServiceConcept.Domain.Responses;

namespace PHP.WebServiceConcept.Domain.Services
{
    internal class TransactionQueryProcesor : ITransactionQueryProcessor
    {
        private ITransactionRepository _transactionRepository;

        public TransactionQueryProcesor(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDetailsResponse> ExecuteQueryAsync(TransactionDetailsQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var transaction = await _transactionRepository.GetByTransactionIdAsync(query.TransactionId);
            return new TransactionDetailsResponse(
                transaction.Account.AccountId,
                transaction.TransactionAmount,
                transaction.TransactionId,
                transaction.TransactionType,
                transaction.ProcessedOnDateTime.HasValue);
        }
    }
}

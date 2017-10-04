using System.Threading.Tasks;
using PHP.WebServiceConcept.Domain.Entities;

namespace PHP.WebServiceConcept.Domain
{
    public interface IAccountRepository
    {
        Task<bool> AccountExistsAsync(string accountId);
        Task<Account> GetAccountAsync(string accountId);
        Task<bool> AddNewAccountAsync(Account newAccount);
        Task<int> SaveChangesAsync();
    }
}

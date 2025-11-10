using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(int id);
        Task<IReadOnlyList<Account>> ListAllAsync();
        Task<Account> AddAsync(Account entity);
        Task UpdateAsync(Account entity);
        Task DeleteAsync(Account entity);
        Task<decimal> GetAccountBalanceAsync(int accountId);
    }
}

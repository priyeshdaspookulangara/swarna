using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan?> GetByIdAsync(int id);
        Task<IReadOnlyList<Loan>> ListAllAsync();
        Task<Loan> AddAsync(Loan entity);
        Task UpdateAsync(Loan entity);
        Task DeleteAsync(Loan entity);
        Task<IReadOnlyList<Loan>> GetLoansByClientIdAsync(int clientId);
        Task<IReadOnlyList<Loan>> GetDefaulterLoansAsync();
        Task<IReadOnlyList<Loan>> GetUpcomingRenewalsAsync(int days);
    }
}

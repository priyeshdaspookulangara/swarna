using GoldLoan.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDto?> GetLoanByIdAsync(int id);
        Task<IReadOnlyList<LoanDto>> GetLoansForClientAsync(int clientId);
        Task<LoanDto> CreateLoanAsync(LoanDto loanDto);
        // Other methods like Update/Delete can be added later if needed
    }
}

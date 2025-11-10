using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface ILoanPlanRepository
    {
        Task<IEnumerable<LoanPlan>> GetAllLoanPlansAsync();
    }
}

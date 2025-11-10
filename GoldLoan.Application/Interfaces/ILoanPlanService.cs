using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface ILoanPlanService
    {
        Task<IEnumerable<LoanPlan>> GetAllLoanPlansAsync();
    }
}

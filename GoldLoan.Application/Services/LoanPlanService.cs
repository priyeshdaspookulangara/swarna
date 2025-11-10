using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Services
{
    public class LoanPlanService : ILoanPlanService
    {
        private readonly ILoanPlanRepository _loanPlanRepository;

        public LoanPlanService(ILoanPlanRepository loanPlanRepository)
        {
            _loanPlanRepository = loanPlanRepository;
        }

        public async Task<IEnumerable<LoanPlan>> GetAllLoanPlansAsync()
        {
            return await _loanPlanRepository.GetAllLoanPlansAsync();
        }
    }
}

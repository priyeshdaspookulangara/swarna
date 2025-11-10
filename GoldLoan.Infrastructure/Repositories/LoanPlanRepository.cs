using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using GoldLoan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Infrastructure.Repositories
{
    public class LoanPlanRepository : ILoanPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanPlan>> GetAllLoanPlansAsync()
        {
            return await _context.LoanPlans.ToListAsync();
        }
    }
}

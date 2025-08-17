using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using GoldLoan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLoan.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            // Include related entities that might be needed
            return await _context.Loans
                .Include(l => l.Client)
                .Include(l => l.CollateralItems)
                .Include(l => l.RepaymentSchedules)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IReadOnlyList<Loan>> ListAllAsync()
        {
            return await _context.Loans
                .Include(l => l.Client)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Loan>> GetLoansByClientIdAsync(int clientId)
        {
            return await _context.Loans
                .Where(l => l.ClientId == clientId)
                .Include(l => l.CollateralItems)
                .ToListAsync();
        }

        public async Task<Loan> AddAsync(Loan entity)
        {
            await _context.Loans.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Loan entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Loan entity)
        {
            _context.Loans.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

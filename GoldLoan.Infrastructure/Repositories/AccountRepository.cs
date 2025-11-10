using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using GoldLoan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<IReadOnlyList<Account>> ListAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> AddAsync(Account entity)
        {
            await _context.Accounts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Account entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Account entity)
        {
            _context.Accounts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetAccountBalanceAsync(int accountId)
        {
            var debits = await _context.JournalEntryLines
                .Where(l => l.AccountId == accountId)
                .SumAsync(l => l.Debit);

            var credits = await _context.JournalEntryLines
                .Where(l => l.AccountId == accountId)
                .SumAsync(l => l.Credit);

            return debits - credits;
        }
    }
}

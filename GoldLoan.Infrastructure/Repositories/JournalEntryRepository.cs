using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using GoldLoan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Infrastructure.Repositories
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public JournalEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JournalEntry> GetByIdAsync(int id)
        {
            return await _context.JournalEntries
                .Include(j => j.Lines)
                .ThenInclude(l => l.Account)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<IReadOnlyList<JournalEntry>> ListAllAsync()
        {
            return await _context.JournalEntries
                .Include(j => j.Lines)
                .ThenInclude(l => l.Account)
                .ToListAsync();
        }

        public async Task<JournalEntry> AddAsync(JournalEntry entity)
        {
            await _context.JournalEntries.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

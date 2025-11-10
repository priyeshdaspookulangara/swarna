using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface IJournalEntryRepository
    {
        Task<JournalEntry> GetByIdAsync(int id);
        Task<IReadOnlyList<JournalEntry>> ListAllAsync();
        Task<JournalEntry> AddAsync(JournalEntry entity);
    }
}

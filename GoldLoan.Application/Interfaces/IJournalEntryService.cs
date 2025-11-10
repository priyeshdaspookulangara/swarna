using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface IJournalEntryService
    {
        Task<JournalEntry> CreateJournalEntryAsync(JournalEntry journalEntry);
        Task<JournalEntry> GetJournalEntryByIdAsync(int id);
        Task<IEnumerable<JournalEntry>> GetAllJournalEntriesAsync();
    }
}

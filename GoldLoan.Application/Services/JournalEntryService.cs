using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IJournalEntryRepository _journalEntryRepository;

        public JournalEntryService(IJournalEntryRepository journalEntryRepository)
        {
            _journalEntryRepository = journalEntryRepository;
        }

        public async Task<JournalEntry> CreateJournalEntryAsync(JournalEntry journalEntry)
        {
            return await _journalEntryRepository.AddAsync(journalEntry);
        }

        public async Task<JournalEntry> GetJournalEntryByIdAsync(int id)
        {
            return await _journalEntryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<JournalEntry>> GetAllJournalEntriesAsync()
        {
            return await _journalEntryRepository.ListAllAsync();
        }
    }
}

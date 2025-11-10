using System;
using System.Collections.Generic;

namespace GoldLoan.Domain.Entities
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Description { get; set; } = null!;
        public ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
    }
}

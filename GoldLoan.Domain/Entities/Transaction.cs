using GoldLoan.Domain.Enums;
using System;

namespace GoldLoan.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
    }
}

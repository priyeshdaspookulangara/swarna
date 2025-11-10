using GoldLoan.Domain.Enums;
using System;

namespace GoldLoan.Application.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
    }
}

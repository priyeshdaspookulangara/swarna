using GoldLoan.Domain.Enums;
using System;

namespace GoldLoan.Domain.Entities
{
    public class RepaymentSchedule
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal PrincipalComponent { get; set; }
        public decimal InterestComponent { get; set; }
        public RepaymentStatus Status { get; set; }
    }
}

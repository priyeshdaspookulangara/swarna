using GoldLoan.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GoldLoan.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public decimal PrincipalAmount { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public int TenureInMonths { get; set; }
        public LoanStatus Status { get; set; }
        public RepaymentType RepaymentType { get; set; }
        public ICollection<CollateralItem> CollateralItems { get; set; } = new List<CollateralItem>();
        public ICollection<RepaymentSchedule> RepaymentSchedules { get; set; } = new List<RepaymentSchedule>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}

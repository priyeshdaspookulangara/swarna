using GoldLoan.Domain.Enums;

namespace GoldLoan.Domain.Entities
{
    public class LoanPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal AnnualInterestRate { get; set; }
        public int TenureInMonths { get; set; }
        public RepaymentType RepaymentType { get; set; }
    }
}

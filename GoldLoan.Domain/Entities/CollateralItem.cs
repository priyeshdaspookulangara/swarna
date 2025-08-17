namespace GoldLoan.Domain.Entities
{
    public class CollateralItem
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal WeightInGrams { get; set; }
        public decimal PurityInKarat { get; set; }
    }
}

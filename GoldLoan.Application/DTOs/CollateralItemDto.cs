namespace GoldLoan.Application.DTOs
{
    public class CollateralItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal WeightInGrams { get; set; }
        public decimal PurityInKarat { get; set; }
    }
}

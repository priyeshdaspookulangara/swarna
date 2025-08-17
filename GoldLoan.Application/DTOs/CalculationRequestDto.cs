namespace GoldLoan.Application.DTOs
{
    public class CalculationRequestDto
    {
        public decimal Principal { get; set; }
        public decimal AnnualRate { get; set; }
        public int TenureInMonths { get; set; }
    }
}

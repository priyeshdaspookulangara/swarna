namespace GoldLoan.Application.Interfaces
{
    public interface IFinancialCalculatorService
    {
        decimal CalculateEmi(decimal principal, decimal annualRate, int tenureInMonths);
        decimal CalculateBulletRepaymentAmount(decimal principal, decimal annualRate, int tenureInMonths);
    }
}

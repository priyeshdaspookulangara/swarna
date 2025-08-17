using GoldLoan.Application.Interfaces;
using System;

namespace GoldLoan.Application.Services
{
    public class FinancialCalculatorService : IFinancialCalculatorService
    {
        public decimal CalculateEmi(decimal principal, decimal annualRate, int tenureInMonths)
        {
            if (principal <= 0 || annualRate <= 0 || tenureInMonths <= 0)
            {
                return 0;
            }

            var monthlyRate = (double)annualRate / 12 / 100;
            var principalDouble = (double)principal;
            var tenureDouble = (double)tenureInMonths;

            var emi = (principalDouble * monthlyRate * Math.Pow(1 + monthlyRate, tenureDouble)) / (Math.Pow(1 + monthlyRate, tenureDouble) - 1);

            return (decimal)emi;
        }

        public decimal CalculateBulletRepaymentAmount(decimal principal, decimal annualRate, int tenureInMonths)
        {
            if (principal <= 0 || annualRate < 0 || tenureInMonths <= 0)
            {
                return principal;
            }

            var monthlyRate = (double)annualRate / 12 / 100;
            var principalDouble = (double)principal;
            var tenureDouble = (double)tenureInMonths;

            var totalAmount = principalDouble * Math.Pow(1 + monthlyRate, tenureDouble);

            return (decimal)totalAmount;
        }
    }
}

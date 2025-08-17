using GoldLoan.Application.Services;
using Xunit;

namespace GoldLoan.Application.Tests.Services
{
    public class FinancialCalculatorServiceTests
    {
        private readonly FinancialCalculatorService _calculator = new FinancialCalculatorService();

        [Theory]
        [InlineData(100000, 12, 12, 8884.88)]
        [InlineData(50000, 10, 24, 2307.25)]
        public void CalculateEmi_ShouldReturnCorrectEmi(double principal, double annualRate, int tenure, double expectedEmi)
        {
            // Arrange
            var principalDec = (decimal)principal;
            var rateDec = (decimal)annualRate;

            // Act
            var result = _calculator.CalculateEmi(principalDec, rateDec, tenure);

            // Assert
            Assert.Equal((decimal)expectedEmi, result, 2);
        }

        [Theory]
        [InlineData(100000, 12, 12, 112682.50)]
        [InlineData(50000, 10, 24, 61019.55)]
        public void CalculateBulletRepaymentAmount_ShouldReturnCorrectAmount(double principal, double annualRate, int tenure, double expectedAmount)
        {
            // Arrange
            var principalDec = (decimal)principal;
            var rateDec = (decimal)annualRate;

            // Act
            var result = _calculator.CalculateBulletRepaymentAmount(principalDec, rateDec, tenure);

            // Assert
            Assert.Equal((decimal)expectedAmount, result, 2);
        }

        [Fact]
        public void CalculateEmi_WithZeroPrincipal_ShouldReturnZero()
        {
            // Act
            var result = _calculator.CalculateEmi(0, 10, 12);
            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateBulletRepaymentAmount_WithZeroPrincipal_ShouldReturnZero()
        {
            // Act
            var result = _calculator.CalculateBulletRepaymentAmount(0, 10, 12);
            // Assert
            Assert.Equal(0, result);
        }
    }
}

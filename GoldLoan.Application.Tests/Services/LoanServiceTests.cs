using GoldLoan.Application.DTOs;
using GoldLoan.Application.Interfaces;
using GoldLoan.Application.Services;
using GoldLoan.Domain.Entities;
using GoldLoan.Domain.Enums;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GoldLoan.Application.Tests.Services
{
    public class LoanServiceTests
    {
        private readonly Mock<ILoanRepository> _mockLoanRepository;
        private readonly Mock<IFinancialCalculatorService> _mockCalculatorService;
        private readonly Mock<IJournalEntryService> _mockJournalEntryService;
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _mockLoanRepository = new Mock<ILoanRepository>();
            _mockCalculatorService = new Mock<IFinancialCalculatorService>();
            _mockJournalEntryService = new Mock<IJournalEntryService>();
            _mockAccountRepository = new Mock<IAccountRepository>();

            var accounts = new List<Account>
            {
                new Account { Id = 1, AccountName = "Loans Receivable" },
                new Account { Id = 2, AccountName = "Cash" }
            };
            _mockAccountRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(accounts);

            _loanService = new LoanService(_mockLoanRepository.Object, _mockCalculatorService.Object, _mockJournalEntryService.Object, _mockAccountRepository.Object);
        }

        [Fact]
        public async Task CreateLoanAsync_WithEmiType_ShouldGenerateRepaymentSchedule()
        {
            // Arrange
            var loanDto = new LoanDto
            {
                PrincipalAmount = 12000,
                AnnualInterestRate = 12,
                TenureInMonths = 12,
                RepaymentType = RepaymentType.EMI
            };

            _mockLoanRepository.Setup(r => r.AddAsync(It.IsAny<Loan>()))
                .ReturnsAsync((Loan l) => {
                    l.Id = 1; // Simulate saving to DB and getting an ID back
                    return l;
                });

            _mockCalculatorService.Setup(c => c.CalculateEmi(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<int>()))
                .Returns(1066.19m);

            // Act
            var result = await _loanService.CreateLoanAsync(loanDto);

            // Assert
            _mockLoanRepository.Verify(r => r.AddAsync(It.Is<Loan>(l =>
                l.RepaymentType == RepaymentType.EMI &&
                l.RepaymentSchedules.Count == 12
            )), Times.Once);
        }

        [Fact]
        public async Task CreateLoanAsync_WithBulletType_ShouldNotGenerateRepaymentSchedule()
        {
            // Arrange
            var loanDto = new LoanDto
            {
                PrincipalAmount = 10000,
                AnnualInterestRate = 10,
                TenureInMonths = 12,
                RepaymentType = RepaymentType.Bullet
            };

            _mockLoanRepository.Setup(r => r.AddAsync(It.IsAny<Loan>()))
                .ReturnsAsync((Loan l) => {
                    l.Id = 1;
                    return l;
                });

            // Act
            var result = await _loanService.CreateLoanAsync(loanDto);

            // Assert
            _mockLoanRepository.Verify(r => r.AddAsync(It.Is<Loan>(l =>
                l.RepaymentType == RepaymentType.Bullet &&
                l.RepaymentSchedules.Count == 0
            )), Times.Once);
            _mockCalculatorService.Verify(c => c.CalculateEmi(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task CreateLoanAsync_ShouldCreateJournalEntry()
        {
            // Arrange
            var loanDto = new LoanDto
            {
                PrincipalAmount = 10000,
                AnnualInterestRate = 10,
                TenureInMonths = 12,
                RepaymentType = RepaymentType.Bullet
            };

            _mockLoanRepository.Setup(r => r.AddAsync(It.IsAny<Loan>()))
                .ReturnsAsync((Loan l) => {
                    l.Id = 1;
                    return l;
                });

            // Act
            var result = await _loanService.CreateLoanAsync(loanDto);

            // Assert
            _mockJournalEntryService.Verify(j => j.CreateJournalEntryAsync(It.Is<JournalEntry>(je =>
                je.Lines.Any(l => l.AccountId == 1 && l.Debit == 10000) &&
                je.Lines.Any(l => l.AccountId == 2 && l.Credit == 10000)
            )), Times.Once);
        }
    }
}

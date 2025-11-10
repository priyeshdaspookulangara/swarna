using GoldLoan.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLoan.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ILoanRepository _loanRepository;

        public StatisticsService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<int> GetTotalLoansAsync()
        {
            var loans = await _loanRepository.ListAllAsync();
            return loans.Count;
        }

        public async Task<decimal> GetTotalLoanAmountAsync()
        {
            var loans = await _loanRepository.ListAllAsync();
            return loans.Sum(l => l.PrincipalAmount);
        }

        public async Task<int> GetActiveLoansAsync()
        {
            var loans = await _loanRepository.ListAllAsync();
            return loans.Count(l => l.Status == Domain.Enums.LoanStatus.Active);
        }
    }
}

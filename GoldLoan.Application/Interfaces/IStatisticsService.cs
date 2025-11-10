using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface IStatisticsService
    {
        Task<int> GetTotalLoansAsync();
        Task<decimal> GetTotalLoanAmountAsync();
        Task<int> GetActiveLoansAsync();
    }
}

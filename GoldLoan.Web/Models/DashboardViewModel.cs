using GoldLoan.Application.DTOs;
using System.Collections.Generic;

namespace GoldLoan.Web.Models
{
    public class DashboardViewModel
    {
        public List<LoanDto> DefaulterLoans { get; set; } = new List<LoanDto>();
        public List<LoanDto> UpcomingRenewals { get; set; } = new List<LoanDto>();
        public int TotalLoans { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public int ActiveLoans { get; set; }
    }
}

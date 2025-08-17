using GoldLoan.Application.DTOs;
using System.Collections.Generic;

namespace GoldLoan.Web.ViewModels
{
    public class LoanIndexViewModel
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public IReadOnlyList<LoanDto> Loans { get; set; } = new List<LoanDto>();
    }
}

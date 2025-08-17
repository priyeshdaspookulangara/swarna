using GoldLoan.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GoldLoan.Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public decimal PrincipalAmount { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public int TenureInMonths { get; set; }
        public LoanStatus Status { get; set; }
        public RepaymentType RepaymentType { get; set; }
        public List<CollateralItemDto> CollateralItems { get; set; } = new List<CollateralItemDto>();
        public List<RepaymentScheduleDto> RepaymentSchedules { get; set; } = new List<RepaymentScheduleDto>();
    }
}

using GoldLoan.Domain.Enums;
using System;

namespace GoldLoan.Application.DTOs
{
    public class RepaymentScheduleDto
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal PrincipalComponent { get; set; }
        public decimal InterestComponent { get; set; }
        public RepaymentStatus Status { get; set; }
    }
}

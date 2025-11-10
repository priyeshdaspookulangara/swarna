using GoldLoan.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoldLoan.Web.ViewModels
{
    public class LoanViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Client")]
        [Required]
        public int ClientId { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; } = new List<SelectListItem>();

        [Display(Name = "Loan Plan")]
        public int? LoanPlanId { get; set; }
        public IEnumerable<SelectListItem> LoanPlans { get; set; } = new List<SelectListItem>();

        [Display(Name = "Principal Amount")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Principal must be a positive number.")]
        public decimal PrincipalAmount { get; set; }

        [Display(Name = "Annual Interest Rate (%)")]
        [Required]
        [Range(0.1, 100, ErrorMessage = "Interest rate must be between 0.1 and 100.")]
        public decimal AnnualInterestRate { get; set; }

        [Display(Name = "Tenure (In Months)")]
        [Required]
        [Range(1, 120, ErrorMessage = "Tenure must be between 1 and 120 months.")]
        public int TenureInMonths { get; set; }

        [Display(Name = "Repayment Type")]
        [Required]
        public RepaymentType RepaymentType { get; set; }

        public List<CollateralItemViewModel> CollateralItems { get; set; } = new List<CollateralItemViewModel>();
    }

    public class CollateralItemViewModel
    {
        [Required]
        public string Description { get; set; } = null!;

        [Display(Name = "Weight (grams)")]
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be a positive number.")]
        public decimal WeightInGrams { get; set; }

        [Display(Name = "Purity (karat)")]
        [Required]
        [Range(1, 24, ErrorMessage = "Purity must be between 1 and 24.")]
        public decimal PurityInKarat { get; set; }
    }
}

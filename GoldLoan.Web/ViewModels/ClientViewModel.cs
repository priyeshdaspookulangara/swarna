using System.ComponentModel.DataAnnotations;

namespace GoldLoan.Web.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = null!;

        [Required]
        [Display(Name = "ID Proof Details")]
        public string IdProofDetails { get; set; } = null!;
    }
}

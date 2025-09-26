using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class FAQ
    {
        [Key]
        [ValidateNever]
        public int FAQId { get; set; }
        [Required(ErrorMessage = "Enter The question")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Enter The Answer")]
        public string Answer { get; set; }
        [ValidateNever]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [ValidateNever]
        public string? CreatedBy { get; set; }
        [ValidateNever]
        public DateTime? UpdatedDate { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; }
    }
}

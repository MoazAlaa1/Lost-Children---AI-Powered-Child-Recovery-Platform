using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class WorkSteps
    {
        [Key]
        [ValidateNever]
        public int WorkStepsId { get; set; }
        [Required(ErrorMessage = "Enter The Step Title")]
        [MaxLength(100, ErrorMessage = "Maximum length of character is 100")]
        public string StepTitle { get; set; }
        [MaxLength(1000, ErrorMessage = "Maximum length of character is 1000")]
        public string? StepDescription { get; set; }
        public string? StepIcon { get; set; }
        [ValidateNever]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [ValidateNever]
        public string? CreatedBy { get; set; }
        [ValidateNever]
        public DateTime? UpdatedDate { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; }
        [ValidateNever]
        public int CurrentState { get; set; } = 1;
    }
}

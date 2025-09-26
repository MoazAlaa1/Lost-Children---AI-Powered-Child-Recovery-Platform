using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class Slider
    {
        [Key]
        [ValidateNever]
        public int SliderId { get; set; }
        [Required(ErrorMessage = "Enter The Title")]
        [MaxLength(150, ErrorMessage = "Maximum length of character is 150")]
        public string Title { get; set; }
        [MaxLength(1000, ErrorMessage = "Maximum length of character is 1000")]
        public string? Description { get; set; }
        public string? SliderImage { get; set; }
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

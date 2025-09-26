using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class AboutSections
    {
        [Key]
        [ValidateNever]
        public int SectionId { get; set; }
        [Required(ErrorMessage = "Enter The Section Name")]
        [MaxLength(100, ErrorMessage = "Maximum length of character is 100")]
        public string SectionName { get; set; }
        [Required(ErrorMessage = "Enter The Story Title")]
        [MaxLength(150, ErrorMessage = "Maximum length of character is 150")]
        public string SectionTitle { get; set; }
        [MaxLength(4000, ErrorMessage = "Maximum length of character is 4000")]
        public string SectionDescription { get; set; }

        public string? SectionImage { get; set; }
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

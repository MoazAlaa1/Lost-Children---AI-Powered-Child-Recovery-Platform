using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LostChildrenGP.Models
{
    public partial class FoundChild
    {
        public FoundChild()
        {
            TbResultChildren = new HashSet<ResultChildren>();
        }
        [Key]
        [ValidateNever]
        public int FoundChildId { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of character is 100")]
        public string? FoundChildName { get; set; }
        [Required(ErrorMessage = "Enter The Child's Photo")]
        public string FoundChildImage { get; set; }
        [ValidateNever]
        public string FC_Embedding { get; set; }
        [Required(ErrorMessage = "Enter The child's Estimated Age")]
        [Range(1,30, ErrorMessage = "Enter The Child's Age in range 0 to 30")]
        public int EstimatedAge { get; set; }
        [Required(ErrorMessage = "Enter The Child's Gender")]
        [MaxLength(100, ErrorMessage = "Maximum length of character is 100")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Enter The Current Condition of Child")]
        [MaxLength(100, ErrorMessage = "Maximum length of character is 100")]
        public string CurrentCondition { get; set; }
        [Required(ErrorMessage = "Enter The Location Where Found the child")]
        [MaxLength(2000, ErrorMessage = "Maximum length of character is 100")]
        public string FoundLocation { get; set; }
        [Required(ErrorMessage = "Enter The Date that you found The child")]
        public DateTime FoundDate { get; set; }
        [MaxLength(2000, ErrorMessage = "Maximum length of character is 100")]
        public string? physicalDescription { get; set; }
        public DateTime? ApproximateTime { get; set; }
        [Required(ErrorMessage = "Enter The Current Location of Child")]
        public string CurrentLocation { get; set; }
        public string? AdditionalNotes { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        public string ReporterName { get; set; }
        [Required(ErrorMessage = "Enter Your Relationship with the child")]
        public string ReporterRelationShip { get; set; }
        [Required(ErrorMessage = "Enter Your Phone Number")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$",ErrorMessage = "Invalid Phone number")]
        //[Length(11,15, ErrorMessage = "Your Phone Number must at least 11 and maxmum 15")]
        public string ReporterPhone { get; set; }
        [EmailAddress]
        [MaxLength(150, ErrorMessage = "Maximum length of character is 150")]
        public string? ReporterEmail { get; set; }
        [ValidateNever]
        public int isFound { get; set; } = 0;
        [ValidateNever]
        public int CurrentState { get; set; } = 1;
        [ValidateNever]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [ValidateNever]
        public DateTime? UpdatedDate { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; }
        public ICollection<ResultChildren> TbResultChildren { get; set; }
        [ValidateNever]
        public int BirthYear { get; set; }


    }

}

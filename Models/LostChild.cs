using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class LostChild
    {
        public LostChild()
        {
            TbSearcResult = new HashSet<SearchResult>();
        }
        [Key]
        [ValidateNever]
        public int LostChildrenId { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [Required(ErrorMessage ="Enter The Child Name")]
        [MaxLength(100, ErrorMessage = "Maximum length of character is 100")]
        public string LostChildrenName { get; set; }
        [Required(ErrorMessage = "Enter The Child Photo")]
        public string LostChildrenImage {  get; set; }
        [ValidateNever]
        public string LC_Embedding { get; set; }
        [Required(ErrorMessage = "Enter The Child's Age in this photo")]
        [Range(1, 18, ErrorMessage = "Enter The Child's Age in range 1 to 18")]
        public int AgeOfPhoto { get; set; }
        [Required(ErrorMessage = "Enter The Child's Birth Date")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Enter The Child's Gender")]
        public string Gender { get; set; }
        [MaxLength(1000, ErrorMessage = "Maximum length of character is 1000")]
        public string? LastSeenLocation { get; set; }
        public DateTime? LastSeenDate { get; set; }
        [MaxLength(2000, ErrorMessage = "Maximum length of character is 2000")]
        public string? PhysicalDescription { get; set; }
        [MaxLength(2000, ErrorMessage = "Maximum length of character is 2000")]
        public string? AdditionalInformation { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        [MaxLength(100, ErrorMessage = "Maximum length of character is 100")]
        public string SearcherName { get; set; }
        [Required(ErrorMessage = "Enter Your Relationship with the child")]
        public string SearcherRelationship { get; set; }
        [Required(ErrorMessage = "Enter Your Phone Number")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid Phone number")]
        public string SearcherPhone { get; set; }
        [EmailAddress]
        [MaxLength(150, ErrorMessage = "Maximum length of character is 150")]
        public string? SearcherEmail { get; set; }
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
        public ICollection<SearchResult> TbSearcResult { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class SearchResult
    {
       
        [Key]
        [ValidateNever]
        public int ResultId { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [ValidateNever]
        public int LostChildId { get; set; }
        
        [ValidateNever]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [ValidateNever]
        public DateTime? UpdatedDate { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; }
        [ValidateNever]
        public int CurrentState { get; set; } = 1;
        public LostChild TblostChild { get; set; } = null!;
        public ICollection<ResultChildren> TbResultChildren { get; set; } = new List<ResultChildren>();
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class ResultChildren
    {
       
        [Key]
        [ValidateNever]
        public int ResultChildrenId { get; set; }
        [ValidateNever]
        public int SearchResultId { get; set; }
        [ValidateNever]
        public int FoundChildId { get; set; }
        [ValidateNever]
        public int? Similarity { get; set; }
        [ValidateNever]
        public int CurrentState { get; set; } = 0;
        public SearchResult TbSearchResult { get; set; } = null!;
        public FoundChild TbFoundChild { get; set; } = null!;


    }
}

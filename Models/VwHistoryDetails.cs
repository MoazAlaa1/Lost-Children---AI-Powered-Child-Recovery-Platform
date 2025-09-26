using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class VwHistoryDetails
    {
        public int ResultChildrenId { get; set; }
        public int SearchResultId { get; set; }
        public int FoundChildId { get; set; }
        public string FoundChildName { get; set; }
        public string FoundChildImage { get; set; }
        public int EstimatedAge { get; set; }
        public string Gender { get; set; }
        public string CurrentCondition { get; set; }
        public string FoundLocation { get; set; }
        public DateTime FoundDate { get; set; }
        public int LostChildId { get; set; }
        public string LostChildrenName { get; set; }
        public string LostChildrenImage { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

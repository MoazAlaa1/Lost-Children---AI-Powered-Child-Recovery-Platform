namespace LostChildrenGP.Models
{
    public class ViewHistory
    {
        public int ResultId { get; set; }
        public string UserId { get; set; }
        public int LostChildId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LostChildrenName { get; set; }
        public string LostChildrenImage { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CurrentState { get; set; } = 1;
    }
}

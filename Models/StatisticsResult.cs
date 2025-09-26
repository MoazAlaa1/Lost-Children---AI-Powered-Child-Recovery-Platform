namespace LostChildrenGP.Models
{
    public class DashboardStatistics
    {
        public int FoundChildren { get; set; }
        public int LostChildren { get; set; }
        public int Users { get; set; }
    }

    public class FoundChildInfo
    {
        public int FoundChildId { get; set; }
        public string FoundChildImage { get; set; }
        public string FoundChildName { get; set; }
        public int EstimatedAge { get; set; }
        public string Gender { get; set; }
        public string CurrentCondition { get; set; }
        public DateTime FoundDate { get; set; }
    }

    public class DashboardResult
    {
        public DashboardStatistics Statistics { get; set; }
        public List<FoundChildInfo> RecentFoundChildren { get; set; }
    }

}

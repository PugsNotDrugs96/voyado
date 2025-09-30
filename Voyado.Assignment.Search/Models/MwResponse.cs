namespace Voyado.Assignment.Search.Models
{
    public class MwResponse 
    {
        public MwQuery? Query { get; set; } 
    }
    public class MwQuery 
    {
        public MwSearchInfo? SearchInfo { get; set; } 
    }
    public class MwSearchInfo 
    { 
        public long TotalHits { get; set; } 
    }
}

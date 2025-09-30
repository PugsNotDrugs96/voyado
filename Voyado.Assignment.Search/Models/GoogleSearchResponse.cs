namespace Voyado.Assignment.Search.Models
{
    public class GoogleSearchResponse
    {
        public SearchInformation? SearchInformation { get; set; }
    }

    public class SearchInformation
    {
        public string? TotalResults { get; set; }
        public string? FormattedTotalResults { get; set; }
    }
}

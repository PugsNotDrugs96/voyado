namespace Voyado.Assignment.Search.Models
{
    public class SearchEngines
    {
        public required GoogleSettings GoogleSettings { get; set; }
        public required MediaWikiSettings MediaWikiSettings { get; set; }
    }
    public class GoogleSettings
    {
        public required string Url { get; set; }
        public required string ApiKey { get; set; }
        public required string CxKey { get; set; }
    }

    public class MediaWikiSettings
    {
        public required string Url { get; set; }
    }
}

using Microsoft.Extensions.Options;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using Voyado.Assignment.Search.Models;

namespace Voyado.Assignment.Search.Services
{
    public class SearchService(
        IConfiguration config,
        IOptionsMonitor<SearchEngines> monitor,
        HttpClient httpClient,
        ILogger<SearchService> logger)
    {
        GoogleSettings googleSettings = monitor.CurrentValue.GoogleSettings;
        MediaWikiSettings mwSettings = monitor.CurrentValue.MediaWikiSettings;


        public async Task<Dictionary<string, long>> GetNbrOfResultsAsync(string searchString)
        {
            Dictionary<string, long> result = new Dictionary<string, long>();

            if (string.IsNullOrWhiteSpace(searchString))
            {
                return result;
            }

            List<string> searchStringSplit = searchString.Split(" ").ToList();

            try
            {
                result.Add("Google", await GetGoogleTotalResultsAsync(searchStringSplit));
                result.Add("MediaWiki", await GetMediaWikiTotalResultsAsync(searchStringSplit));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public async Task<long> GetGoogleTotalResultsAsync(List<string> searchStringSplit)
        {
            long totalResults = 0;
            foreach (string searchWord in searchStringSplit)
            {
                string googleUri = $"{googleSettings.Url}{Uri.EscapeDataString(searchWord)}&key={googleSettings.ApiKey}" +$"&cx={googleSettings.CxKey}";
                GoogleSearchResponse? data = await httpClient.GetFromJsonAsync<GoogleSearchResponse>(googleUri);

                if (long.TryParse(data?.SearchInformation?.TotalResults, NumberStyles.Integer, CultureInfo.InvariantCulture, out var count))
                {
                    totalResults += count;
                }
                else
                {
                    logger.LogError("totalResults missing/ invalid for google uri: {uri}", googleUri);
                }
            }

            return totalResults;
        }

        public async Task<long> GetMediaWikiTotalResultsAsync(List<string> searchStringSplit, CancellationToken ct = default)
        {
            long totalResults = 0;

            //MediaWiki required User-Agent header
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            using HttpClient httpClient = new HttpClient(handler);

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                $"VoyadoSearch/1.0 (+mailto:{config.GetValue<string>("ApplicationMail")})"
            );

            foreach (string searchWord in searchStringSplit)
            {
                string mwUri = $"{mwSettings.Url}{Uri.EscapeDataString(searchWord)}";
                MwResponse? data = await httpClient.GetFromJsonAsync<MwResponse>(mwUri, ct);

                if (data == null)
                {
                    logger.LogError("totalResults missing/ invalid for MediaWiki uri: {uri}", mwUri);
                    continue;
                }

                totalResults += data.Query?.SearchInfo?.TotalHits ?? 0;
            }

            return totalResults;
        }
    }
}

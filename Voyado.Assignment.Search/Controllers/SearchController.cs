using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voyado.Assignment.Search.Models;
using Voyado.Assignment.Search.Services;

namespace Voyado.Assignment.Search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController(
        SearchService searchService,
        ILogger<SearchController> logger) : ControllerBase
    {
        [HttpGet("GetNbrOfResults/{searchString}")]
        public async Task<Dictionary<string, long>> GetNbrOfResults(string searchString)
        {
            return await searchService.GetNbrOfResultsAsync(searchString);
        }
    }
}

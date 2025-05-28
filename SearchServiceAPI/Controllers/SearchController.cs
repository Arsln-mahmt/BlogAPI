using Microsoft.AspNetCore.Mvc;
using SearchServiceAPI.Models;
using SearchServiceAPI.Services;

namespace SearchService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        // GET: /api/search?query=uzay
        [HttpGet]
        public IActionResult Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { message = "Sorgu boş olamaz." });

            var results = _searchService.SearchPosts(query);
            return Ok(results);
        }
    }
}

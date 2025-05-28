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

        [HttpGet("suggestions")]
        public IActionResult GetSuggestions([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Ok(new List<string>());

            var results = _searchService
                .SearchPosts(query)
                .Select(p => p.Title)
                .Where(t => !string.IsNullOrEmpty(t) && t.ToLower().Contains(query.ToLower()))
                .Distinct()
                .Take(5)
                .ToList();

            return Ok(results);
        }




    }
}

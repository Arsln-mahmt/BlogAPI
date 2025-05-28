using SearchServiceAPI.Models;


namespace SearchServiceAPI.Services
{
    public interface ISearchService
    {
        List<PostSearchResultDto> SearchPosts(string query);
    }
}

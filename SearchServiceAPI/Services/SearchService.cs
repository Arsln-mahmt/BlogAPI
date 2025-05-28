using SearchServiceAPI.Models;

namespace SearchServiceAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly List<PostSearchResultDto> _mockPosts = new()
        {
            new PostSearchResultDto { Id = 1, Title = "Uzay Yolculuğu", Content = "Mars’a ilk insanlı görev başlıyor", Topic = "Uzay", Author = "mahmut", CreatedAt = DateTime.UtcNow },
            new PostSearchResultDto { Id = 2, Title = "Yapay Zeka", Content = "GPT modelleri hayatımızı değiştiriyor", Topic = "Teknoloji", Author = "buket", CreatedAt = DateTime.UtcNow },
            new PostSearchResultDto { Id = 3, Title = "Evren", Content = "Evrenin sınırları hakkında yeni keşifler", Topic = "Bilim", Author = "mahmut", CreatedAt = DateTime.UtcNow }
        };

        public List<PostSearchResultDto> SearchPosts(string query)
        {
            return _mockPosts
                .Where(p =>
                    p.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    p.Content.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    p.Topic.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}

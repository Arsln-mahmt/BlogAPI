using SearchServiceAPI.Models;

namespace SearchServiceAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly List<PostSearchResultDto> _mockPosts = new()
        {
            new PostSearchResultDto
            {
                Id = 1,
                Title = "Uzay Yolculuğu",
                Content = "Mars’a ilk insanlı görev başlıyor",
                Topic = "Uzay",
                Author = "mahmut",
                CreatedAt = DateTime.UtcNow
            },
            new PostSearchResultDto
            {
                Id = 2,
                Title = "Yapay Zeka",
                Content = "GPT modelleri hayatımızı değiştiriyor",
                Topic = "Teknoloji",
                Author = "buket",
                CreatedAt = DateTime.UtcNow
            },
            new PostSearchResultDto
            {
                Id = 3,
                Title = "Fenerbahçe",
                Content = "Ali Koç istifa etmeden şampiyonluk zor gibi.",
                Topic = "Spor",
                Author = "ali",
                CreatedAt = DateTime.UtcNow
            },
            new PostSearchResultDto
            {
                Id = 4,
                Title = "Evren",
                Content = "Evrenin sınırları hakkında yeni keşifler",
                Topic = "Bilim",
                Author = "mahmut",
                CreatedAt = DateTime.UtcNow
            }
        };

        public List<PostSearchResultDto> SearchPosts(string query)
        {
            return _mockPosts
                .Where(p =>
                    (!string.IsNullOrEmpty(p.Title) && p.Title.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(p.Content) && p.Content.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(p.Topic) && p.Topic.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }
}

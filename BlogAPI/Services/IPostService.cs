using BlogAPI.DTOs;
using BlogAPI.Entities;

namespace BlogAPI.Services
{
    public interface IPostService
    {
        // Tüm postları getir (giriş gerektirmez)
        Task<IEnumerable<Post>> GetAllPostsAsync();

        // ID ile sade post getir (düzenleme için)
        Task<Post?> GetPostByIdAsync(int id);

        // Post detay DTO (okuma ekranı için — yazar adı vs. içerir)
        Task<PostDetailDto> GetPostDetailsByIdAsync(int id);

        // Yeni post oluştur
        Task<Post> CreatePostAsync(CreatePostDto dto, string userId);

        // Post silme işlemi (admin veya yazar yetkilidir)
        Task<bool> DeletePostAsync(int id, string userId);

        Task<IEnumerable<Post>> GetPostsByTopicAsync(string title);

        Task<IEnumerable<string>> GetAllTopicsAsync();

        Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId);





    }
}

using BlogAPI.DTOs;
using BlogAPI.Entities;
using BlogAPI.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.User).ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PostDetailDto?> GetPostDetailsByIdAsync(int id)
        {
            var post = await _context.Posts
                .Include(p => p.User) // Kullanıcı bilgisi için önemli
                .Where(p => p.Id == id)
                .Select(p => new PostDetailDto
                {
                    Title = p.Title,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    FullName = p.User.FullName,
                    AuthorId = p.User.Id // 🔥 Artık null dönmeyecek
                })
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task<Post> CreatePostAsync(CreatePostDto dto, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("❌ userId boş geliyor! Token'dan gelen kullanıcı ID null.");
            }

            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = userId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> DeletePostAsync(int id, string userId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (post == null) return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Post>> GetPostsByTopicAsync(string title)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Where(p => p.Title.ToLower() == title.ToLower())
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllTopicsAsync()
        {
            return await _context.Posts
                .Select(p => p.Title)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();
        }



    }
}

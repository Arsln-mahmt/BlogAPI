namespace BlogAPI.DTOs
{
    public class PostDetailDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; }
        public string AuthorId { get; set; }

    }
}

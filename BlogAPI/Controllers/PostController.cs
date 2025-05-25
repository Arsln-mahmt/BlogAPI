using BlogAPI.DTOs;
using BlogAPI.Entities;
using BlogAPI.Identity;
using BlogAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id); // sade detay içermeyen versiyon
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var post = await _postService.GetPostDetailsByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("❌ HATA: userId null veya boş geliyor!");

            var newPost = await _postService.CreatePostAsync(dto, userId);
            return CreatedAtAction(nameof(GetDetailById), new { id = newPost.Id }, newPost);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var success = await _postService.DeletePostAsync(id, userId);
            if (!success) return Forbid();
            return NoContent();
        }

        [HttpGet("by-topic")]
        public async Task<IActionResult> GetByTopic([FromQuery] string title)
        {
            var posts = await _postService.GetPostsByTopicAsync(title);
            return Ok(posts);
        }

        [HttpGet("topics")]
        public async Task<IActionResult> GetTopics()
        {
            var topics = await _postService.GetAllTopicsAsync();
            return Ok(topics);
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyPosts()
        {
            var userId = _userManager.GetUserId(User);
            var posts = await _postService.GetPostsByUserIdAsync(userId);
            return Ok(posts);
        }



    }
}

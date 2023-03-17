using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class PostController : Controller
    {
        private readonly PostService _postService;


        public PostController(PostService postService)
        {
            _postService = postService;
        }
        [HttpGet(Name = "GetAllPosts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            return posts;
        }
        [HttpGet("{id}", Name = "GetPost")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            return post;
        }

        [HttpPost(Name = "CreatePost")]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            await _postService.PostPost(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> PutPost(int id, Post post)
        {
            var existingPost = await _postService.PutPost(id, post);
            return await _postService.GetPost(id);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePost(id);
            return NoContent();
        }
    }
}
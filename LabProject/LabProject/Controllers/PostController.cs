using LabProject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
            var resPosts = await _postService.GetPosts();
            var posts = resPosts.Value.ToList();
            var response = new BaseResponse<Post>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = posts
            };
            return Ok(response);
        }
        [HttpGet("{id}", Name = "GetPost")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var resPost = await _postService.GetPost(id);
            var post = resPost.Value;
            var response = new BaseResponse<Post>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = new List<Post> { post }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPost(Name = "CreatePost")]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            await _postService.PostPost(post);
            var response = new BaseResponse<Post>()
            {
                Description = "Created",
                StatusCode = 201,
                Values = new List<Post> { post }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> PutPost(int id, Post post)
        {
            var existingPost = await _postService.PutPost(id, post);
            var response = new BaseResponse<Post>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = new List<Post> { post }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePost(id);
            var response = new BaseResponse<Post>()
            {
                Description = "No content",
                StatusCode = 204
            };
            return Ok(response);
        }
    }
}
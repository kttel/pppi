using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyLabProject
{
    [ApiController]
    [Route("[controller]s")]
    public class PostController : ControllerBase
    {
        IPostRepository PostRepository;

        public PostController(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }
        [HttpGet(Name = "GetAllPosts")]
        public IEnumerable<Post> Get()
        {
            return PostRepository.Get();
        }
        [HttpGet("{Id}", Name = "GetPost")]
        public IActionResult Get(int Id)
        {
            Post post = PostRepository.Get(Id);

            if (post == null)
            {
                return NotFound();
            }

            return new ObjectResult(post);
        }

        [HttpPost(Name = "CreatePost")]
        public IActionResult Create([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            PostRepository.Create(post);
            return CreatedAtRoute("GetPost", new { id = post.Id }, post);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int Id, [FromBody] Post updatedPost)
        {
            if (updatedPost == null || updatedPost.Id != Id)
            {
                return BadRequest();
            }

            var author = PostRepository.Get(Id);
            if (author == null)
            {
                return NotFound();
            }

            PostRepository.Update(updatedPost);
            return RedirectToRoute("GetAllPosts");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var deletedPost = PostRepository.Delete(Id);

            if (deletedPost == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedPost);
        }
    }
}

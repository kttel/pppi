using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LabProject
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var resAuthors = await _authorService.GetAuthors();
            var authors = resAuthors.Value.ToList();
            var response = new BaseResponse<Author>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = authors
            };
            return Ok(response);
        }
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var resAuthor = await _authorService.GetAuthor(id);
            var author = resAuthor.Value;
            var response = new BaseResponse<Author>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = new List<Author> { author }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            await _authorService.CreateAuthor(author);
            var response = new BaseResponse<Author>()
            {
                Description = "Created",
                StatusCode = 201,
                Values = new List<Author> { author }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> PutAuthor(int id, Author author)
        {
            await _authorService.PutAuthor(id, author);
            var response = new BaseResponse<Author>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = new List<Author> { author }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthor(id);
            var response = new BaseResponse<Author>()
            {
                Description = "No content",
                StatusCode = 204
            };
            return Ok(response);
        }
    }
}
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
        // private readonly AuthorService _authorService;
        IAuthorRepository AuthorRepository;

        public AuthorController(IAuthorRepository authorRepository)//AuthorService authorService)
        {
            //_authorService = authorService;
            AuthorRepository = authorRepository;
        }
        [HttpGet(Name = "GetAllAuthors")]
        public IEnumerable<Author> Get()//async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return AuthorRepository.Get();
            //var resAuthors = await _authorService.GetAuthors();
            //var authors = resAuthors.Value.ToList();
            //var response = new BaseResponse<Author>()
            //{
            //    Description = "Success",
            //    StatusCode = 200,
            //    Values = authors
            //};
            //return Ok(response);
        }
        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult Get(int Id)//async Task<ActionResult<Author>> GetAuthor(int id)
        {
            Author author = AuthorRepository.Get(Id);

            if (author == null)
            {
                return NotFound();
            }

            return new ObjectResult(author);
            //var resAuthor = await _authorService.GetAuthor(id);
            //var author = resAuthor.Value;
            //var response = new BaseResponse<Author>()
            //{
            //    Description = "Success",
            //    StatusCode = 200,
            //    Values = new List<Author> { author }
            //};
            //return Ok(response);
        }

        [Authorize]
        [HttpPost(Name = "CreateAuthor")]
        public IActionResult Create([FromBody] Author author)//async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            AuthorRepository.Create(author);
            return CreatedAtRoute("GetAuthor", new { id = author.Id }, author);
            //await _authorService.CreateAuthor(author);
            //var response = new BaseResponse<Author>()
            //{
            //    Description = "Created",
            //    StatusCode = 201,
            //    Values = new List<Author> { author }
            //};
            //return Ok(response);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int Id, [FromBody] Author updatedAuthor)
        {
            if (updatedAuthor == null || updatedAuthor.Id != Id)
            {
                return BadRequest();
            }

            var author = AuthorRepository.Get(Id);
            if (author == null)
            {
                return NotFound();
            }

            AuthorRepository.Update(updatedAuthor);
            return RedirectToRoute("GetAllAuthors");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var deletedAuthor = AuthorRepository.Delete(Id);

            if (deletedAuthor == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedAuthor);
        }

        //    [Authorize]
        //    [HttpPut("{id}")]
        //    public async Task<ActionResult<Author>> PutAuthor(int id, Author author)
        //    {
        //        await _authorService.PutAuthor(id, author);
        //        var response = new BaseResponse<Author>()
        //        {
        //            Description = "Success",
        //            StatusCode = 200,
        //            Values = new List<Author> { author }
        //        };
        //        return Ok(response);
        //    }

        //    [Authorize]
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteAuthor(int id)
        //    {
        //        await _authorService.DeleteAuthor(id);
        //        var response = new BaseResponse<Author>()
        //        {
        //            Description = "No content",
        //            StatusCode = 204
        //        };
        //        return Ok(response);
        //    }
        //}
    }
}
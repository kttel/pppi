using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyLabProject.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        IAuthorRepository AuthorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
        }
        [HttpGet(Name = "GetAllAuthors")]
        public IEnumerable<Author> Get()
        {
            return AuthorRepository.Get();
        }
        [HttpGet("{Id}", Name = "GetAuthor")]
        public IActionResult Get(int Id)
        {
            Author author = AuthorRepository.Get(Id);

            if (author == null)
            {
                return NotFound();
            }

            return new ObjectResult(author);
        }

        [HttpPost(Name = "CreateAuthor")]
        public IActionResult Create([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            AuthorRepository.Create(author);
            return CreatedAtRoute("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("{Id}")]
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

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var deletedAuthor = AuthorRepository.Delete(Id);

            if (deletedAuthor == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedAuthor);
        }
    }
}

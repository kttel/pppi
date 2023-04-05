using Microsoft.AspNetCore.Mvc;
using LabProject;

namespace LabProject
{
    public class AuthorService : IAuthorService
    {
        private static readonly List<Author> authors = new List<Author>
        {
            new Author(1, "First user", "ktt.eli.z.abeth@gmail.com"),
            new Author(2, "Second user", "kst.lbeez@gmail.com"),
            new Author(3, "Admin", "admin@example.com"),
            new Author(4, "Moderator", "moderator@example.com"),
            new Author(5, "Bob", "cleancodemastery@example.com"),
            new Author(6, "System", "system@mywebsite.com"),
            new Author(7, "Anonymous user", "anon@gmail.com"),
            new Author(8, "_pythonist_", "pythonmyprecious@example.com"),
            new Author(9, "Greg", "user@example.com"),
            new Author(10, "Larry", "larry@example.com")
        };

        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            await Task.Run(() =>
            {
                authors.Add(author);
            });
            return author;
        }

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await Task.Run(() => authors.RemoveAll(x => x.Id == id));
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<Author>> GetAuthor(int id)
        {

            var author = authors.Find(x => x.Id == id);
            await Task.FromResult(author);
            return author;
        }

        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            await Task.FromResult(authors);
            return authors;
        }

        public async Task<ActionResult<Author>> PutAuthor(int id, Author author)
        {
            var existingAuthor = authors.FirstOrDefault(x => x.Id == id);

            await Task.Run(() =>
            {
                existingAuthor.Username = author.Username;
                existingAuthor.Email = author.Email;
            });
            return new NoContentResult();
        }
    }
}

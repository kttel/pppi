using Microsoft.AspNetCore.Mvc;
using LabProject;

namespace LabProject
{
    public interface IAuthorService
    {
        Task<ActionResult<IEnumerable<Author>>> GetAuthors();
        Task<ActionResult<Author>> GetAuthor(int id);
        Task<ActionResult<Author>> CreateAuthor(Author author);
        Task<IActionResult> DeleteAuthor(int id);
        Task<ActionResult<Author>> PutAuthor(int id, Author author);
    }
}

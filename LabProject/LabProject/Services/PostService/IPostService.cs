using Microsoft.AspNetCore.Mvc;
using LabProject;

namespace LabProject
{
    public interface IPostService
    {
        Task<ActionResult<IEnumerable<Post>>> GetPosts();
        Task<ActionResult<Post>> GetPost(int id);
        Task<ActionResult<Post>> PostPost(Post post);
        Task<ActionResult<Post>> PutPost(int id, Post post);
        Task<IActionResult> DeletePost(int id);
    }
}

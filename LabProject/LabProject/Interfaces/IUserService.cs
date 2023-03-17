using Microsoft.AspNetCore.Mvc;

namespace LabProject
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<ActionResult<User>> GetUser(int id);
        Task<ActionResult<User>> CreateUser(User user);
        Task<IActionResult> DeleteUser(int id);
        Task<ActionResult<User>> PutUser(int id, User user);
    }
}

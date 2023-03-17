using Microsoft.AspNetCore.Mvc;

namespace LabProject
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return users;
        }
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            return user;
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            return await _userService.CreateUser(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            await _userService.PutUser(id, user);
            return await _userService.GetUser(id);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
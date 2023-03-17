using Microsoft.AspNetCore.Mvc;

namespace LabProject
{
    public class UserService : IUserService
    {
        private static readonly List<User> users = new List<User>
        {
            new User(1, "First user", "ktt.eli.z.abeth@gmail.com"),
            new User(2, "Second user", "kst.lbeez@gmail.com"),
            new User(3, "Admin", "admin@example.com"),
            new User(4, "Moderator", "moderator@example.com"),
            new User(5, "Bob", "cleancodemastery@example.com"),
            new User(6, "System", "system@mywebsite.com"),
            new User(7, "Anonymous user", "anon@gmail.com"),
            new User(8, "_pythonist_", "pythonmyprecious@example.com"),
            new User(9, "Greg", "user@example.com"),
            new User(10, "Larry", "larry@example.com")
        };

        public async Task<ActionResult<User>> CreateUser(User user)
        {
            await Task.Run(() =>
            {
                users.Add(user);
            });
            return user;
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await Task.Run(() => users.RemoveAll(x => x.Id == id));
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<User>> GetUser(int id)
        {

            var user = users.Find(x => x.Id == id);
            await Task.FromResult(user);
            return user;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            await Task.FromResult(users);
            return users;
        }

        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            var existingUser = users.FirstOrDefault(x => x.Id == id);

            await Task.Run(() =>
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
            });
            return new NoContentResult();
        }
    }
}

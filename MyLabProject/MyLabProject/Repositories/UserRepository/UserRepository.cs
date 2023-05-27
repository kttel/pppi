using Microsoft.EntityFrameworkCore;

namespace MyLabProject
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task IncrementFailedAttempts(User user)
        {
            user.FailedAuthorizationAttempts++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task ResetFailedAttempts(User user)
        {
            user.FailedAuthorizationAttempts = 0;
            await _dbContext.SaveChangesAsync();
        }
    }
}

namespace LabProject
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task UpdateUser(User user);
        Task IncrementFailedAttempts(User user);
        Task ResetFailedAttempts(User user);
    }
}

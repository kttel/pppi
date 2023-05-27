namespace MyLabProject
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterRequest request);
        Task<string> Login(UserLoginRequest request);
    }
}

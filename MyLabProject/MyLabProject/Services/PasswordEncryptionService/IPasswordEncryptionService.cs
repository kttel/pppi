namespace MyLabProject
{
    public interface IPasswordEncryptionService
    {
        string EncryptPassword(string password);
        bool VerifyPassword(string plainTextPassword, string hashedPassword);
    }
}

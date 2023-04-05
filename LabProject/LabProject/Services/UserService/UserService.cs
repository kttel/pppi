using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LabProject
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryptionService _encryptionService;

        public UserService(IUserRepository userRepository, IPasswordEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        public async Task<User> Register(UserRegisterRequest request)
        {
            var existingUser = await _userRepository.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("A user with this email already exists");
            }

            var encryptedPassword = _encryptionService.EncryptPassword(request.Password);
            var user = new User
            {
                Name = request.Name,
                SecondName = request.SecondName,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Password = encryptedPassword
            };

            var createdUser = await _userRepository.CreateUser(user);
            return createdUser;
        }

        public async Task<string> Login(UserLoginRequest request)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new ArgumentException("Invalid email or password");
            }

            var isPasswordValid = _encryptionService.VerifyPassword(request.Password, user.Password);
            if (!isPasswordValid)
            {
                await _userRepository.IncrementFailedAttempts(user);
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            await _userRepository.ResetFailedAttempts(user);
            user.LastAuthorizationDate = DateTime.UtcNow;
            await _userRepository.UpdateUser(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("my-32-character-ultra-secure-and-ultra-long-secret");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

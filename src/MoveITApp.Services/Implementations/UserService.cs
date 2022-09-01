using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Models;
using MoveITApp.Helpers;
using MoveITApp.Mappers;
using MoveITApp.Services.Interfaces;
using MoveITApp.Shared.AppSettings;
using MoveITApp.Shared.CustomExceptions;
using MovieITApp.Dtos.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoveITApp.Services.Implementations
{
    /// <summary>
    /// Contains business logic for managing users
    /// </summary>
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IOptions<AuthenticationSettings> _options;

        public UserService(IUserRepository userRepository, IOptions<AuthenticationSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }

        /// <inheritdoc />
        public async Task<string> LoginUser(LoginUserDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.UserName) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new BadDataException("Username and password are required fields!");
            }

            var hash = DataSecurityHelper.GenerateHash(loginDto.Password);
            User userDb = await _userRepository.LoginUserAsync(loginDto.UserName, hash);
            if (userDb == null)
            {
                throw new ResourceNotFoundException("User not found");
            }

            //GENERATE JWT TOKEN
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.JWTSecret);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                   {
                        new Claim(ClaimTypes.Name, userDb.Username)
                    }
                )
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        /// <inheritdoc />
        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            await ValidateUser(registerUserDto);

            var hash = DataSecurityHelper.GenerateHash(registerUserDto.Password);

            User user = registerUserDto.ToUser(hash);
            await _userRepository.AddAsync(user);
        }

        private async Task ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmedPassword))
            {
                throw new InvalidDataException("Username and password are required fields!");
            }
            if (registerUserDto.Username.Length > 30)
            {
                throw new InvalidDataException("Username: Maximum length for username is 30 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.FirstName) && registerUserDto.FirstName.Length > 50)
            {
                throw new InvalidDataException("Maximum length for FirstName is 50 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.LastName) && registerUserDto.LastName.Length > 50)
            {
                throw new InvalidDataException("Maximum length for LastName is 50 characters");
            }
            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new InvalidDataException("Passwords must match!");
            }

            var userDb = await _userRepository.GetUserByUsernameAsync(registerUserDto.Username);
            if (userDb != null)
            {
                throw new InvalidDataException($"The username {registerUserDto.Username} is already in use!");
            }
        }
    }
}

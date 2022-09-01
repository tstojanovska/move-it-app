using MovieITApp.Dtos.Users;

namespace MoveITApp.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Registers user - Creates a new User record
        /// </summary>
        /// <param name="registerUserDto">Data for the new user</param>
        Task RegisterUser(RegisterUserDto registerUserDto);
        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <param name="loginDto">Login data</param>
        Task<string> LoginUser(LoginUserDto loginDto);
    }
}

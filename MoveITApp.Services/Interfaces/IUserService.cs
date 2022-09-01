using MovieITApp.Dtos.Users;

namespace MoveITApp.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
        Task<string> LoginUser(LoginUserDto loginDto);
    }
}

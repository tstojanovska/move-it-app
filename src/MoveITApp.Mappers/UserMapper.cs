using MoveITApp.Domain.Models;
using MovieITApp.Dtos.Users;

namespace MoveITApp.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto registerUserDto, string hashedPassword)
        {
            return new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Password = hashedPassword
            };
        }
    }
}

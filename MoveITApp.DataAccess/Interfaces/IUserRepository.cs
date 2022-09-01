using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Get a user by username
        /// </summary>
        /// <param name="username">Username of the user</param>
        Task<User> GetUserByUsernameAsync(string username);
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <param name="hashedPassword">The hashed value of the password</param>
        Task<User> LoginUserAsync(string username, string hashedPassword);
    }
}

using Microsoft.EntityFrameworkCore;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Implementations
{
    /// <summary>
    /// Contains methods for managing data access for Users 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private MoveITDbContext _moveItDbContext;

        public UserRepository(MoveITDbContext moveItDbContext)
        {
            _moveItDbContext = moveItDbContext;
        }

        /// <inheritdoc />
        public async Task AddAsync(User entity)
        {
            _moveItDbContext.Users.Add(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(User entity)
        {
            _moveItDbContext.Users.Remove(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<User>> GetAllAsync()
        {
            return await _moveItDbContext.Users.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<User> GetByIdAsync(int id)
        {
            return await _moveItDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _moveItDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        }

        /// <inheritdoc />
        public async Task<User> LoginUserAsync(string username, string hashedPassword)
        {
            return await _moveItDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower()
            && x.Password == hashedPassword);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(User entity)
        {
            _moveItDbContext.Users.Update(entity);
            await _moveItDbContext.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Enums;
using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Implementations
{
    /// <summary>
    /// Contains methods for managing data access for MovingObjectRules 
    /// </summary>
    public class MovingObjectRuleRepository : IMovingObjectRuleRepository
    {
        private MoveITDbContext _moveItDbContext;

        public MovingObjectRuleRepository(MoveITDbContext moveItDbContext)
        {
            _moveItDbContext = moveItDbContext;
        }

        /// <inheritdoc />
        public async Task AddAsync(MovingObjectRule entity)
        {
            _moveItDbContext.MovingObjectRules.Add(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(MovingObjectRule entity)
        {
            _moveItDbContext.MovingObjectRules.Remove(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<MovingObjectRule>> GetAllAsync()
        {
            return await _moveItDbContext.MovingObjectRules.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<MovingObjectRule> GetByIdAsync(int id)
        {
            return await _moveItDbContext.MovingObjectRules.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<MovingObjectRule> GetMovingObjectRuleByTypeAsync(MovingObjectType movingObjectType)
        {
            return await _moveItDbContext.MovingObjectRules.FirstOrDefaultAsync(x => x.MovingObjectType == movingObjectType);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(MovingObjectRule entity)
        {
            _moveItDbContext.MovingObjectRules.Update(entity);
            await _moveItDbContext.SaveChangesAsync();
        }
    }
}

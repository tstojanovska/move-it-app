using Microsoft.EntityFrameworkCore;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Enums;
using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Implementations
{
    public class MovingObjectRuleRepository : IMovingObjectRuleRepository
    {
        private MoveITDbContext _moveItDbContext;

        public MovingObjectRuleRepository(MoveITDbContext moveItDbContext)
        {
            _moveItDbContext = moveItDbContext;
        }
        public async Task AddAsync(MovingObjectRule entity)
        {
            _moveItDbContext.MovingObjectRules.Add(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(MovingObjectRule entity)
        {
            _moveItDbContext.MovingObjectRules.Remove(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        public async Task<List<MovingObjectRule>> GetAllAsync()
        {
            return await _moveItDbContext.MovingObjectRules.ToListAsync();
        }

        public async Task<MovingObjectRule> GetByIdAsync(int id)
        {
            return await _moveItDbContext.MovingObjectRules.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MovingObjectRule> GetMovingObjectRuleByTypeAsync(MovingObjectType movingObjectType)
        {
            return await _moveItDbContext.MovingObjectRules.FirstOrDefaultAsync(x => x.MovingObjectType == movingObjectType);
        }

        public async Task UpdateAsync(MovingObjectRule entity)
        {
            _moveItDbContext.MovingObjectRules.Update(entity);
            await _moveItDbContext.SaveChangesAsync();
        }
    }
}

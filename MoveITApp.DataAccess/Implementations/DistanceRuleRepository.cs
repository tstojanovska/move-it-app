using Microsoft.EntityFrameworkCore;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Implementations
{
    public class DistanceRuleRepository : IDistanceRuleRepository
    {
        private MoveITDbContext _moveItDbContext;

        public DistanceRuleRepository(MoveITDbContext moveItDbContext)
        {
            _moveItDbContext = moveItDbContext;
        }
        public async Task AddAsync(DistanceRule entity)
        {
            _moveItDbContext.DistanceRules.Add(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(DistanceRule entity)
        {
            _moveItDbContext.DistanceRules.Remove(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        public async Task<List<DistanceRule>> GetAllAsync()
        {
            return await _moveItDbContext.DistanceRules.ToListAsync();
        }

        public async Task<DistanceRule> GetByIdAsync(int id)
        {
            return await _moveItDbContext.DistanceRules.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DistanceRule> GetDistanceRuleByRangeAsync(int distance)
        {
            return await _moveItDbContext.DistanceRules.FirstOrDefaultAsync(x => x.From <= distance && (x.To == null || x.To >= distance));
        }

        public async Task UpdateAsync(DistanceRule entity)
        {
            _moveItDbContext.DistanceRules.Update(entity);
            await _moveItDbContext.SaveChangesAsync();
        }
    }
}

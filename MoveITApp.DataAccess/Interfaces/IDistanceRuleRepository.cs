

using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Interfaces
{
    public interface IDistanceRuleRepository : IRepository<DistanceRule>
    {
        Task<DistanceRule> GetDistanceRuleByRangeAsync(int distance);
    }
}

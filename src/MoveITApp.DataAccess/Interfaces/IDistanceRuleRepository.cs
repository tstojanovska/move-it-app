using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Interfaces
{
    public interface IDistanceRuleRepository : IRepository<DistanceRule>
    {
        /// <summary>
        /// Get distance rule by distance range
        /// </summary>
        /// <param name="distance">Distance that needs to be passed</param>
        Task<DistanceRule> GetDistanceRuleByRangeAsync(int distance);
    }
}

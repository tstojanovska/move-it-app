using MoveITApp.Domain.Enums;
using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Interfaces
{
    public interface IMovingObjectRuleRepository : IRepository<MovingObjectRule>
    {
        /// <summary>
        /// Get moving object rule by type
        /// </summary>
        /// <param name="movingObjectType">The type of the object to be moved</param>
        Task<MovingObjectRule> GetMovingObjectRuleByTypeAsync(MovingObjectType movingObjectType);
    }
}

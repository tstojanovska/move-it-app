using MoveITApp.Domain.Enums;
using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Interfaces
{
    public interface IMovingObjectRuleRepository : IRepository<MovingObjectRule>
    {
        Task<MovingObjectRule> GetMovingObjectRuleByTypeAsync(MovingObjectType movingObjectType);
    }
}

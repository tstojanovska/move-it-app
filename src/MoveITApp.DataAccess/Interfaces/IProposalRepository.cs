using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Interfaces
{
    public interface IProposalRepository : IRepository<Proposal>
    {
        /// <summary>
        /// Gets from the db all the proposals for a given user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<List<Proposal>> GetUserProposalsAsync(int userId);
    }
}

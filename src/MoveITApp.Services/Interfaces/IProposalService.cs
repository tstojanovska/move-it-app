using MovieITApp.Dtos.Proposals;

namespace MoveITApp.Services.Interfaces
{
    public interface IProposalService
    {
        /// <summary>
        /// Initiates a proposal and saves it to db
        /// </summary>
        /// <param name="initiateProposalDto">Data needed for a proposal</param>
        /// <param name="username">Username of the user who asks for proposal</param>
        Task<ProposalDto> InitiateProposalAsync(InitiateProposalDto initiateProposalDto, string username);
        /// <summary>
        /// Returns all the proposals of a user
        /// </summary>
        /// <param name="username">User username</param>
        Task<List<ProposalDto>> GetUserProposalsAsync(string username);
        /// <summary>
        /// Returns proposal details
        /// </summary>
        /// <param name="id">Proposal id</param>
        /// <param name="username">User username</param>
        Task<ProposalDto> GetUserProposalDetailsAsync(int id, string username);
    }
}

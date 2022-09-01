using MovieITApp.Dtos.Proposals;

namespace MoveITApp.Services.Interfaces
{
    public interface IProposalService
    {
        Task<ProposalDto> GetProposal(InitiateProposalDto initiateProposalDto, string username);
    }
}

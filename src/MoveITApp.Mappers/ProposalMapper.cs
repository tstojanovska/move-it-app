using MoveITApp.Domain.Models;
using MovieITApp.Dtos.Proposals;

namespace MoveITApp.Mappers
{
    /// <summary>
    /// Contains methods for mapping between Proposal domain objects and DTO-s
    /// </summary>
    public static class ProposalMapper
    {
        public static Proposal ToProposal(this ProposalDto proposalDto)
        {
            return new Proposal
            {
                AtticAreaVolume = proposalDto.AtticAreaVolume,
                CalculatedPrice = proposalDto.CalculatedPrice,
                Distance = proposalDto.Distance,
                LivingAreaVolume = proposalDto.LivingAreaVolume,
                MovingObjectType = proposalDto.MovingObjectType,
                UserId = proposalDto.UserId
            };
        }
    }
}

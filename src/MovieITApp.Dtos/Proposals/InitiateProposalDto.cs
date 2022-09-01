using MoveITApp.Domain.Enums;

namespace MovieITApp.Dtos.Proposals
{
    //Contains data needed for a new proposal
    public class InitiateProposalDto
    {
        /// <summary>
        /// Distance to be covered when moving
        /// </summary>
        public int Distance { get; set; }
        /// <summary>
        /// Living area to be moved
        /// </summary>
        public int LivingAreaVolume { get; set; }
        /// <summary>
        /// Attic area to be moved
        /// </summary>
        public int AtticAreaVolume { get; set; }
        /// <summary>
        /// Possible additional object that needs to be moved
        /// </summary>
        public MovingObjectType? MovingObjectType { get; set; }
    }
}

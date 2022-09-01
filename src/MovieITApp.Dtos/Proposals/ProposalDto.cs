using MoveITApp.Domain.Enums;

namespace MovieITApp.Dtos.Proposals
{
    /// <summary>
    /// Data for a proposal returned to the client
    /// </summary>
    public class ProposalDto
    {
        /// <summary>
        /// Id of the user who made the proposal
        /// </summary>
        public int UserId { get; set; }
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
        /// <summary>
        /// Calucalted price for moving according the rules and requirements
        /// </summary>
        public decimal CalculatedPrice { get; set; }
    }
}

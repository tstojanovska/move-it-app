using MoveITApp.Domain.Enums;

namespace MovieITApp.Dtos.Proposals
{
    public class InitiateProposalDto
    {
        public int Distance { get; set; }
        public int LivingAreaVolume { get; set; }
        public int AtticAreaVolume { get; set; }
        public MovingObjectType? MovingObjectType { get; set; }
    }
}

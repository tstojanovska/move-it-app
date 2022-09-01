using MoveITApp.Domain.Enums;

namespace MovieITApp.Dtos.Proposals
{
    public class ProposalDto
    {
        public int UserId { get; set; }
        public int Distance { get; set; }
        public int LivingAreaVolume { get; set; }
        public int AtticAreaVolume { get; set; }
        public MovingObjectType? MovingObjectType { get; set; }
        public decimal CalculatedPrice { get; set; }
    }
}

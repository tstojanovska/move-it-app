using MoveITApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoveITApp.Domain.Models
{
    public class Proposal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Distance { get; set; }
        public int LivingAreaVolume { get; set; } = 0;
        public int AtticAreaVolume { get; set; } = 0;
        public MovingObjectType? MovingObjectType { get; set; }
        public decimal CalculatedPrice { get; set; }
    }
}

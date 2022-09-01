using MoveITApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoveITApp.Domain.Models
{
    public class MovingObjectRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public MovingObjectType MovingObjectType { get; set; }
        public int FixedPrice { get; set; }
    }
}

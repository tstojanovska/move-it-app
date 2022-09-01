using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoveITApp.Domain.Models
{
    public class DistanceRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int From { get; set; }
        public int? To { get; set; }
        public int FixedPrice { get; set; }
        public int PricePerKm { get; set; }
    }
}

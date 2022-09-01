using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoveITApp.Domain.Models
{
    /// <summary>
    /// Rule for calculating price according a distance range
    /// </summary>
    public class DistanceRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Starting value of a distance range
        /// </summary>
        public int From { get; set; }
        /// <summary>
        /// End value of a distance range
        /// </summary>
        public int? To { get; set; }
        /// <summary>
        /// Fixed price for a distance range
        /// </summary>
        public int FixedPrice { get; set; }
        /// <summary>
        /// Price per km for a distance range
        /// </summary>
        public int PricePerKm { get; set; }
    }
}

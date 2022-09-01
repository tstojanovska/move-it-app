using MoveITApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoveITApp.Domain.Models
{
    /// <summary>
    /// Rule for returning fixed price for moving an object of a specific type
    /// </summary>
    public class MovingObjectRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Type of the object that needs to be moved
        /// </summary>
        public MovingObjectType MovingObjectType { get; set; }
        /// <summary>
        /// Fixed price for moving the object
        /// </summary>
        public int FixedPrice { get; set; }
    }
}

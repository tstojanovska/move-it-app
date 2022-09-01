using MoveITApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoveITApp.Domain.Models
{
    /// <summary>
    /// Proposal made to the end user as a result of a requirement
    /// </summary>
    public class Proposal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Id of the user who made the proposal
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The user who made the proposal
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// The distance that needs to be covered
        /// </summary>
        public int Distance { get; set; }
        /// <summary>
        /// The volume of the living area
        /// </summary>
        public int LivingAreaVolume { get; set; } = 0;
        /// <summary>
        /// The volume of the attic area
        /// </summary>
        public int AtticAreaVolume { get; set; } = 0;
        /// <summary>
        /// Type of the additional moving object. 
        /// </summary>
        public MovingObjectType? MovingObjectType { get; set; }
        /// <summary>
        /// The calculated price for moving according the rules and requirements
        /// </summary>
        public decimal CalculatedPrice { get; set; }
    }
}

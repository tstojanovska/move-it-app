using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoveITApp.Domain.Models
{
    /// <summary>
    /// End user
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// List of user proposals
        /// </summary>
        public IEnumerable<Proposal> Proposals { get; set; }

        public User()
        {
            Proposals = new List<Proposal>();
        }
    }
}

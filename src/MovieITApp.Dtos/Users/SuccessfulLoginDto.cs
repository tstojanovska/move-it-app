

namespace MovieITApp.Dtos.Users
{
    /// <summary>
    /// Holds data generated after successful login
    /// </summary>
    public class SuccessfulLoginDto
    {
        /// <summary>
        /// JWT token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }
    }
}

namespace MovieITApp.Dtos.Users
{
    /// <summary>
    /// Data for registering a user
    /// </summary>
    public class RegisterUserDto
    {
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name
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
        /// Confirmed password
        /// </summary>
        public string ConfirmedPassword { get; set; }
    }
}

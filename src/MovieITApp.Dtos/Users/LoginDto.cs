namespace MovieITApp.Dtos.Users
{
    /// <summary>
    /// Data for logging in a user
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}



namespace MoveITApp.Shared.CustomExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string? message = null) : base(message)
        {
        }
    }
}

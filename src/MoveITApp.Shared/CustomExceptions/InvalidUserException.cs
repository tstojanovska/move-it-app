

namespace MoveITApp.Shared.CustomExceptions
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException(string? message = null) : base(message)
        {
        }
    }
}

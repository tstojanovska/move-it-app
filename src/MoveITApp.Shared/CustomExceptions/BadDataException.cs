

namespace MoveITApp.Shared.CustomExceptions
{
    public class BadDataException : Exception
    {
        public BadDataException(string? message) : base(message)
        {
        }
    }
}

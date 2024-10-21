namespace ApiProject.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        const string DefaultMessage = "Item not found";

        public NotFoundException() : this(DefaultMessage)
        {
        }

        public NotFoundException(string? message) : this(message, new Exception("No further information was given"))
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

namespace Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message) { }
    }

    public class InvalidOperationException : Exception
    {
        public InvalidOperationException(string message)
            : base(message) { }
    }

    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string message)
            : base(message) { }
    }
}

namespace uni_cap_pro_be.Core.Exceptions
{
    public class NotFoundException(string message) : Exception(message) { }

    public class InvalidOperationException(string message) : Exception(message) { }

    public class UnauthorizedAccessException(string message) : Exception(message) { }

    public class UnauthenticatedException(string message) : Exception(message) { }
}

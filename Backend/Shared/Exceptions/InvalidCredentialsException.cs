namespace Shared;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException(string message = "Invalid Credentials")
        : base(message) { }
}

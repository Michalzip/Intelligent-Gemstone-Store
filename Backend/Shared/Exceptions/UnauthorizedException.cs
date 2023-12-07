namespace Shared;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message = "UnAuthorized")
        : base(message) { }
}

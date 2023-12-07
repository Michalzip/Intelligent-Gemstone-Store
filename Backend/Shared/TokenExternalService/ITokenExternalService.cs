namespace Shared;

public interface ITokenExternalService
{
    public string ReturnToken(string jwtCacheKey);
}

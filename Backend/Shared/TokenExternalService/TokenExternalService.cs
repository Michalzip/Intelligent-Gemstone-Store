using Shared.Storage;

namespace Shared;

public class TokenExternalService : ITokenExternalService
{
    private readonly IRequestStorage _requestStorage;

    public TokenExternalService(IRequestStorage requestStorage)
    {
        _requestStorage = requestStorage;
    }

    public string ReturnToken(string jwtCacheKey)
    {
        string jwtToken = _requestStorage.GetCookie(jwtCacheKey);
        return jwtToken;
    }
}

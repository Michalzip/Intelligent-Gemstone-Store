using Microsoft.Extensions.Caching.Memory;

namespace Shared.Storage
{
    internal class RequestStorage : IRequestStorage
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session;
        private HttpResponse _response;
        private HttpRequest _request;

        public RequestStorage(IMemoryCache cache, IHttpContextAccessor httpContextAccessor)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _response = httpContextAccessor.HttpContext.Response;
            _request = httpContextAccessor.HttpContext.Request;
        }

        public void SetCache<T>(string key, T value, TimeSpan? duration = null) =>
            _cache.Set(key, value, duration ?? TimeSpan.FromMinutes(5));

        public T GetCache<T>(string key) => _cache.Get<T>(key);

        public void SetSession(string key, string value) => _session.SetString(key, value);

        public string GetSession(string key) => _session.GetString(key);

        //STORAGE FROM BROWSER SIDE:

        public void SetCookie(string key, string value, int? expireSeconds = null)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddSeconds(expireSeconds ?? 30000),
                IsEssential = true,
            };
            _response.Cookies.Append(key, value, option);
        }

        public string GetCookie(string key)
        {
            return _request.Cookies[key];
        }
    }
}

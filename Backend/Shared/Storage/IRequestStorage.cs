namespace Shared.Storage
{
    public interface IRequestStorage
    {
        void SetCache<T>(string key, T value, TimeSpan? duration = null);
        T GetCache<T>(string key);

        void SetSession(string key, string value);
        string GetSession(string key);

        public void SetCookie(string key, string value, int? expireSeconds = null);

        public string GetCookie(string key);
    }
}

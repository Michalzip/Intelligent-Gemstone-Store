using System.Text;
using Newtonsoft.Json.Linq;
using Integrations.Ebay.Configuration;
using Shared.Http;
using IntelligentStore.Application.External.Auth;
using Shared.Storage;
using IntelligentStore.Integrations.Ebay.Endpoints;
using Shared;

namespace IntelligentStore.Integrations.Ebay.Authorization
{
    public class EbayAuthorization : IExternalServiceAuthorization
    {
        private readonly IConfigurationEbay _configurationEbay;
        private readonly IHttpRequests _httpRequests;
        private readonly IRequestStorage _requestStorage;
        private readonly string base64Credentials;
        private readonly ITokenExternalService _tokenExternalService;

        public EbayAuthorization(
            IConfigurationEbay configurationEbay,
            IHttpRequests httpRequests,
            IRequestStorage requestStorage,
            ITokenExternalService tokenExternalService
        )
        {
            _configurationEbay = configurationEbay;
            _httpRequests = httpRequests;
            _requestStorage = requestStorage;
            _tokenExternalService = tokenExternalService;
            base64Credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(
                    $"{_configurationEbay.ClientId}:{_configurationEbay.ClientSecret}"
                )
            );
        }

        public async Task Auth()
        {
            var headers = PrepareHeaders();

            var responseMessage = await GetAccessToken(headers);

            string responseStringMessage = await responseMessage.Content.ReadAsStringAsync();

            var jsonObject = JObject.Parse(responseStringMessage);

            string jwtToken = jsonObject.Value<string>("access_token");

            int expires_in = jsonObject.Value<int>("expires_in");

            _httpRequests.AddBearerTokenHeader(jwtToken);

            StoreJwtToken(jwtToken, expires_in);
        }

        public bool DoesJwtTokenExist()
        {
            string jwtToken = _tokenExternalService.ReturnToken(CookieKeys.EBAY_JWT_KEY);

            return !string.IsNullOrEmpty(jwtToken);
        }

        private Dictionary<string, string> PrepareHeaders()
        {
            return new Dictionary<string, string>
            {
                { "Authorization", $"Basic {base64Credentials}" },
                { "Content-Type", "application/x-www-form-urlencoded" },
                { "Accept", "application/json" }
            };
        }

        private async Task<HttpResponseMessage> GetAccessToken(Dictionary<string, string> headers)
        {
            var scopes = new List<string> { "https://api.ebay.com/oauth/api_scope", };

            var encodedScopes = string.Join(" ", scopes.ConvertAll(Uri.EscapeDataString));

            var parameters = $"grant_type=client_credentials&scope={encodedScopes}";

            var content = new StringContent(
                parameters,
                Encoding.UTF8,
                "application/x-www-form-urlencoded"
            );

            return await _httpRequests.ApiPostCallAsync(
                $"{EbayEndpoints.GetAccessTokenEndpoint}",
                content,
                headers
            );
        }

        private void StoreJwtToken(string jwtToken, int expiresIn)
        {
            _requestStorage.SetCookie(CookieKeys.EBAY_JWT_KEY, jwtToken, expiresIn);
        }
    }
}

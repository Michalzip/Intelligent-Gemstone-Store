using Integrations.Ebay.Contracts;
using IntelligentStore.Application.External;
using IntelligentStore.Integrations.Ebay.Authorization;
using IntelligentStore.Integrations.Ebay.Endpoints;
using Newtonsoft.Json.Linq;
using Shared;
using Shared.Http;
using Shared.Providers;
using Shared.Storage;

namespace IntelligentStore.Integrations.Ebay.Services
{
    public sealed class EbayService : IExternalService
    {
        public string SERVICE_NAME { get; } = "Ebay";

        public string SERVICE_JWT_KEY { get; } = CookieKeys.EBAY_JWT_KEY;

        private readonly IHttpRequests _httpRequests;
        private readonly ITokenExternalService _tokenExternalService;

        public EbayService(IHttpRequests httpRequests, ITokenExternalService tokenExternalService)
        {
            _httpRequests = httpRequests;
            _tokenExternalService = tokenExternalService;
        }

        private string GetJwtToken()
        {
            return _tokenExternalService.ReturnToken(SERVICE_JWT_KEY);
        }

        private string SetEndpoint(
            string phrase,
            decimal? startingPrice,
            decimal? finalPrice,
            int offset,
            int limitContent
        )
        {
            string endpoint =
                $"{EbayEndpoints.GetGemstonesByCategoryName}&offset={offset}&limit={limitContent}";

            if (phrase != null)
                endpoint += $"&aspect_filter=categoryId:262027,Gemstone Type:" + "{" + phrase + "}";

            if (startingPrice.Value != 0 || finalPrice.Value != 0)
                endpoint += $"&filter=price:[{startingPrice}..{finalPrice}],priceCurrency:USD";

            return endpoint;
        }

        public async Task<List<GemstoneModelProvider>> GetData(
            string phrase,
            decimal? startingPrice,
            decimal? finalPrice,
            int offset,
            int limitContent
        )
        {
            string jwtKey = GetJwtToken();

            string endpoint = SetEndpoint(phrase, startingPrice, finalPrice, offset, limitContent);

            _httpRequests.AddBearerTokenHeader(jwtKey);

            var gemstoneData = await _httpRequests.ApiFetchCallAsync(endpoint, null);

            var gemsontDataAsJson = JObject.Parse(gemstoneData);

            var itemSummaries = gemsontDataAsJson.GetValue("itemSummaries") as JArray;

            List<EbayContract> contracts = itemSummaries.ToObject<List<EbayContract>>();

            List<GemstoneModelProvider> gemstoneProviders = new List<GemstoneModelProvider>();

            foreach (var item in contracts)
            {
                gemstoneProviders.Add(
                    new GemstoneModelProvider(
                        item.itemId,
                        item.title,
                        item.image.imageUrl,
                        item.price.value,
                        item.marketingPrice?.discountAmount.value,
                        item.marketingPrice?.originalPrice.value,
                        item.seller.feedbackPercentage,
                        item.seller.feedbackScore
                    )
                );
            }

            return gemstoneProviders;
        }
    }
}

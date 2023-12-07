namespace IntelligentStore.Integrations.Ebay.Endpoints
{
    public static class EbayEndpoints
    {
        public static string GetAccessTokenEndpoint { get; } =
            "https://api.ebay.com/identity/v1/oauth2/token";

        //example : https://api.ebay.com/buy/browse/v1/item/v1|186110638306|0"
        public static string GetGemstoneById { get; } =
            "https://api.ebay.com/buy/browse/v1/item/v1/";
        public static string GetGemstonesByCategoryName { get; } =
            "https://api.ebay.com/buy/browse/v1/item_summary/search?q=gemstone&category_ids=262027";
    }
}

using Newtonsoft.Json;

namespace Integrations.Ebay.Contracts
{
    public class EbayContract
    {
        public required string itemId { get; set; }

        public required string title { get; set; }

        public required CurrentPrice price { get; set; }

        public Seller? seller;
        public MarketingPrice? marketingPrice { get; set; }

        public required Image image { get; set; }
    }

    public class CurrentPrice
    {
        public required string value { get; set; }
    }

    public class Seller
    {
        public required string feedbackPercentage { get; set; }
        public required int feedbackScore { get; set; }
    }

    public class MarketingPrice
    {
        public required OriginalPrice originalPrice { get; set; }

        public required DiscoutPrice discountAmount { get; set; }
    }

    public class OriginalPrice
    {
        public required string value { get; set; }
    }

    public class DiscoutPrice
    {
        public required string value { get; set; }
    }

    public class Image
    {
        public required string imageUrl { get; set; }
    }
}

using System;

namespace Shared.Providers
{
    public class GemstoneModelProvider
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CurrentPrice { get; set; }
        public string DiscountPrice { get; set; }
        public string OriginalPrice { get; set; }
        public string FeedbackPercentage { get; set; }
        public int FeedbackScore { get; set; }

        public GemstoneModelProvider(
            string id,
            string name,
            string image,
            string currentPrice,
            string discountPrice,
            string originalPrice,
            string feedbackPercentage,
            int feedbackScore
        )
        {
            Id = id;
            Name = name;
            Image = image;
            CurrentPrice = currentPrice;
            DiscountPrice = discountPrice;
            OriginalPrice = originalPrice;
            FeedbackPercentage = feedbackPercentage;
            FeedbackScore = feedbackScore;
        }
    }
}

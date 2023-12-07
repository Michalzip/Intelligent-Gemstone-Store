namespace IntelligentStore.Application.DTOs
{
    public class PriceDto
    {
        public string? Phrase { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int PageNumber { get; set; } = 0;
    }
}

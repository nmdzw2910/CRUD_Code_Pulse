namespace CodePulse.API.Models.DTO
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}

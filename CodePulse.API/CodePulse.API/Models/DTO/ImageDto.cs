namespace CodePulse.API.Models.Domain
{
    public class ImageDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}

namespace CodePulse.API.Models.Domain
{
    public class Product : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public List<ProductImage>? ProductImages { get; set; } = new List<ProductImage>();
    }
}

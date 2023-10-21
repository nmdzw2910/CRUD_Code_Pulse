using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CodePulse.API.Models.DTO
{
    public class ProductDto
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public List<ProductImage>? ProductImages { get; set; } = new List<ProductImage>();
    }
}

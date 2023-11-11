using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Services
{
    public interface IProductService
    {
        Task<List<ProductImage>> UploadImages(IFormFileCollection files);
        Task<ProductDto> Upsert(ProductDto product, IFormFileCollection images);
    }
}

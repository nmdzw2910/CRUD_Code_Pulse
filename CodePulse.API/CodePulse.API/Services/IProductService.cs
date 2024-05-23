using CodePulse.API.Models.DTO;

namespace CodePulse.API.Services
{
    public interface IProductService
    {
        Task<ProductDto> Upsert(ProductDto product, IFormFileCollection images);
    }
}

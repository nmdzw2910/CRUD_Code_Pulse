using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Mappers
{
    public class ProductMapper: Profile
    {
        public ProductMapper() {
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ProductImages, opt => opt.Ignore());
            CreateMap<Product, ProductDto>();
        }
    }
}

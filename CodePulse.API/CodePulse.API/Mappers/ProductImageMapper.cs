using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Mappers
{
    public class ProductImageMapper : Profile
    {
        public ProductImageMapper()
        {
            CreateMap<ProductImageDto, ProductImage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ProductImage, ProductImageDto>();
        }
    }
}

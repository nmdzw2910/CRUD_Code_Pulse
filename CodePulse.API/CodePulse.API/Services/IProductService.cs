﻿using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Services
{
    public interface IProductService
    {
        Task<ProductDto> Create(ProductDto request);
        Task<ProductDto> Update(Product existingProduct, ProductDto request);
        Task<List<ProductImage>> UploadImages(IFormFileCollection files);
        Task<ProductDto> Upsert(ProductDto product);
    }
}

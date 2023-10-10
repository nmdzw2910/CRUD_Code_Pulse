﻿using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task<Product> UpdateAsync(Product product);
        Task<Product> DeleteAsync(Product product);
    }
}

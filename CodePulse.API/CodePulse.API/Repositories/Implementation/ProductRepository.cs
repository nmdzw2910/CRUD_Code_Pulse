using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var categories = await _dbContext.Products.Include(p => p.ProductImages).ToListAsync();

            return categories;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await _dbContext.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _dbContext.Attach(product);
            _dbContext.Entry(product).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteAsync(Product product)
        {
            if (product.ProductImages != null)
            {
                _dbContext.ProductImages.RemoveRange(product.ProductImages);
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}

using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            List<Product> categories = await dbContext.Products.Include(p => p.Images).ToListAsync();

            return categories;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await dbContext.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            dbContext.Attach(product);
            dbContext.Entry(product).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteAsync(Product product)
        {
            if (product.Images != null)
            {
                dbContext.Images.RemoveRange(product.Images);
            }
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return product;
        }
    }
}

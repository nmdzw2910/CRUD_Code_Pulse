using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>?> GetAllAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            // Attach the category to the DbContext and mark it as modified
            _dbContext.Attach(category);
            _dbContext.Entry(category).State = EntityState.Modified;

            // Save changes to the database
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteAsync(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}

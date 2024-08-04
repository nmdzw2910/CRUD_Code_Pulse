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
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetAllAsync()
        {
            return await _dbContext.Categories.Select(c => c.Name).ToListAsync();
        }

        public async Task<string> GetByNameAsync(string name)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
            return category?.Name;
        }

        public async Task<string> CreateAsync(string name)
        {
            var existingCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Name == name);

            if (existingCategory != null)
            {
                return existingCategory.Name;
            }

            var category = new Category { Name = name };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category.Name;
        }

        public async Task<string> UpdateAsync(string oldName, string newName)
        {
            var existingCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Name == newName);

            if (existingCategory != null)
            {
                return existingCategory.Name;
            }

            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Name == oldName);

            if (category != null)
            {
                var newCategory = new Category { Name = newName };
                _dbContext.Categories.Add(newCategory);

                _dbContext.Categories.Remove(category);

                await _dbContext.SaveChangesAsync();
                return newCategory.Name;
            }

            return null;
        }


        public async Task<string> DeleteAsync(string name)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
                return category.Name;
            }
            return null;
        }
    }
}

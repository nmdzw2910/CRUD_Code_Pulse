using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            List<Category> categories = await dbContext.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            // Attach the category to the DbContext and mark it as modified
            dbContext.Attach(category);
            dbContext.Entry(category).State = EntityState.Modified;

            // Save changes to the database
            await dbContext.SaveChangesAsync();
        }
    }
}

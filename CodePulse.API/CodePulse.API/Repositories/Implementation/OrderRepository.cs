using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Order>> GetAllAsync()
        {
            List<Order> categories = await dbContext.Orders.Include(p => p.OrderDetails).ToListAsync();

            return categories;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var order = await dbContext.Orders.Include(p => p.OrderDetails).FirstOrDefaultAsync(p => p.Id == id);
            return order;
        }
    }
}

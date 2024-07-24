using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Order>?> GetAllAsync()
        {
            List<Order>? categories = await _dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.ShippingInformation)
                .ToListAsync();

            return categories;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.ShippingInformation)
                .FirstOrDefaultAsync(p => p.Id == id);
            return order;
        }

        public async Task<Order?> GetByOrderNumberAsync(string orderNumber)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.ShippingInformation)
                .FirstOrDefaultAsync(p => p.OrderNumber == orderNumber);
            return order;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _dbContext.Attach(order);
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.Entry(order.ShippingInformation).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteAsync(Order order)
        {
            _dbContext.OrderDetails.RemoveRange(order.OrderDetails);

            _dbContext.ShippingInformation.Remove(order.ShippingInformation);

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<string?> GetLastOrderNumberForDateAsync(string currentDate)
        {
            {
                var lastOrderNumber = await _dbContext.Orders
                    .Where(o => o.OrderNumber != null && o.OrderNumber.StartsWith(currentDate))
                    .OrderByDescending(o => o.OrderNumber)
                    .Select(o => o.OrderNumber)
                    .FirstOrDefaultAsync();

                return lastOrderNumber;
            }
        }
    }
}

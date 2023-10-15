using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Order>> GetAllAsync()
        {
            List<Order> categories = await dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.ShippingInformation)
                .ToListAsync();

            return categories;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var order = await dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.ShippingInformation)
                .FirstOrDefaultAsync(p => p.Id == id);
            return order;
        }

        public async Task<Order> GetByOrderNumberAsync(string orderNumber)
        {
            var order = await dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.ShippingInformation)
                .FirstOrDefaultAsync(p => p.OrderNumber == orderNumber);
            return order;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            dbContext.Attach(order);
            dbContext.Entry(order).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteAsync(Order order)
        {
            if (order.OrderDetails != null)
            {
                dbContext.OrderDetails.RemoveRange(order.OrderDetails);
            }

            if (order.ShippingInformation != null)
            {
                dbContext.ShippingInformations.Remove(order.ShippingInformation);
            }

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<string> GetLastOrderNumberForDateAsync(string currentDate)
        {
            {
                var lastOrderNumber = await dbContext.Orders
                    .Where(o => o.OrderNumber.StartsWith(currentDate))
                    .OrderByDescending(o => o.OrderNumber)
                    .Select(o => o.OrderNumber)
                    .FirstOrDefaultAsync();

                return lastOrderNumber;
            }
        }
    }
}

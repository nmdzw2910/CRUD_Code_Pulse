using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>?> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order?> GetByOrderNumberAsync(string orderNumber);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<Order> DeleteAsync(Order order);
        Task<string?> GetLastOrderNumberForDateAsync(string currentDate);
    }
}

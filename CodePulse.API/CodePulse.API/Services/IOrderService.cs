using CodePulse.API.Models.DTO;

namespace CodePulse.API.Services
{
    public interface IOrderService
    {
        Task<OrderDto> Upsert(OrderDto orderDto);
    }
}

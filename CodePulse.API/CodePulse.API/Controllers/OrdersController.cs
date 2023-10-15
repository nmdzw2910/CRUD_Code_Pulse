using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await orderRepository.GetAllAsync();

            if (orders == null || orders.Count == 0)
            {
                return NotFound("No orders found.");
            }

            var orderDtos = orders.ConvertAll(order => new OrderDto
            {
                Id = order.Id,
                OrderStatus = order.OrderStatus,
                TotalAmount = order.TotalAmount,
                ShippingInformation = order.ShippingInformation,
                PaymentMethod = order.PaymentMethod,
                OrderDetails = order.OrderDetails,
                CreatedAt = order.CreatedAt,
            });

            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            var orderDto = new OrderDto
            {
                Id = order.Id,
                OrderStatus = order.OrderStatus,
                TotalAmount = order.TotalAmount,
                ShippingInformation = order.ShippingInformation,
                PaymentMethod = order.PaymentMethod,
                OrderDetails = order.OrderDetails,
                CreatedAt = order.CreatedAt,
            };

            return Ok(orderDto);
        }
    }
}

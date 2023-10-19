using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderService orderService;

        public OrdersController(IOrderRepository orderRepository, IOrderService orderService)
        {
            this.orderRepository = orderRepository;
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await orderRepository.GetAllAsync();

            if (orders == null || orders.Count == 0)
            {
                return NotFound("No orders found.");
            }

            var orderDtos = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
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
                OrderNumber = order.OrderNumber,
                OrderStatus = order.OrderStatus,
                TotalAmount = order.TotalAmount,
                ShippingInformation = order.ShippingInformation,
                PaymentMethod = order.PaymentMethod,
                OrderDetails = order.OrderDetails,
                CreatedAt = order.CreatedAt,
            };

            return Ok(orderDto);
        }

        [HttpGet("orderNumber/{orderNumber}")]
        public async Task<IActionResult> GetOrderByOrderNumber(string orderNumber)
        {
            var order = await orderRepository.GetByOrderNumberAsync(orderNumber);

            if (order == null)
            {
                return NotFound($"Order with OrderNumber {orderNumber} not found.");
            }

            var orderDto = new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderStatus = order.OrderStatus,
                TotalAmount = order.TotalAmount,
                ShippingInformation = order.ShippingInformation,
                PaymentMethod = order.PaymentMethod,
                OrderDetails = order.OrderDetails,
                CreatedAt = order.CreatedAt,
            };

            return Ok(orderDto);
        }

        /// <summary>
        /// Creates or updates an Order. If the optional id is provided in the body, the existing Order with that id is overwritten.
        /// If it is not provided, then a new Order is created, and an id is generated.
        /// </summary>
        /// <param name="order">
        /// Body of a request must contain all necessary data about Order that will be created/updated.
        /// The Id attribute is optional and when provided the existing Order with that id is overwritten.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> UpsertOrder(OrderDto request)
        {
            var response = await orderService.Upsert(request);

            return Ok(response);
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderById(Guid id)
        {
            var existingOrder = await orderRepository.GetByIdAsync(id);

            if (existingOrder == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            await orderRepository.DeleteAsync(existingOrder);

            return Ok($"Order {existingOrder.OrderNumber} has been deleted.");
        }

        // DELETE: api/orders
        [HttpDelete]
        public async Task<IActionResult> BulkDeleteOrders(List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("No order IDs provided for deletion.");
            }

            foreach (var id in ids)
            {
                var existingProduct = await orderRepository.GetByIdAsync(id);

                if (existingProduct != null)
                {
                    await orderRepository.DeleteAsync(existingProduct);
                }
            }

            return Ok($"Deleted {ids.Count} orders.");
        }
    }
}

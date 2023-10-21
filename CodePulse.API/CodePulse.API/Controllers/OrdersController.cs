using AutoMapper;
using CodePulse.API.Models.DTO;
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
        private readonly IMapper mapper;

        public OrdersController(IOrderRepository orderRepository, IOrderService orderService, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderService = orderService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retrieve a list of all orders.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await orderRepository.GetAllAsync();

            if (orders == null || orders.Count == 0)
            {
                return NotFound("No orders found.");
            }
            var orderDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(orderDtos);
        }

        /// <summary>
        /// Retrieve an order by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the order.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            var orderDto = mapper.Map<OrderDto>(order);

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
            var orderDto = mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        /// <summary>
        /// Create or update an Order. If the optional id is provided in the body, the existing Order with that id is overwritten.
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

        /// <summary>
        /// Delete an order by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the order to delete.</param>
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

        /// <summary>
        /// Delete multiple orders by their unique identifiers.
        /// </summary>
        /// <param name="ids">A collection of unique identifiers for the orders to delete.</param>
        [HttpDelete("bulkdelete")]
        public async Task<IActionResult> BulkDeleteOrders([FromBody] List<Guid> ids)
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

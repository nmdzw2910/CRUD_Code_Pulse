using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderRepository">IOrderRepository.</param>
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<OrderDto> Upsert(OrderDto orderDto)
        {
            var existingOrder = await orderRepository.GetByIdAsync(orderDto.Id);

            if (orderDto.Id != Guid.Empty && existingOrder != null)
            {
                return await Update(existingOrder, orderDto);
            }
            return await Create(orderDto);
        }

        public async Task<OrderDto> Update(Order existingOrder, OrderDto request)
        {
            existingOrder.OrderStatus = request.OrderStatus;
            existingOrder.OrderNumber = request.OrderNumber;
            existingOrder.TotalAmount = request.TotalAmount;
            existingOrder.ShippingInformation = request.ShippingInformation;
            existingOrder.PaymentMethod = request.PaymentMethod;
            existingOrder.OrderDetails = request.OrderDetails;
            existingOrder.UpdatedAt = DateTime.Now;

            await orderRepository.UpdateAsync(existingOrder);

            var response = new OrderDto
            {
                Id = existingOrder.Id,
                OrderNumber = existingOrder.OrderNumber,
                OrderStatus = existingOrder.OrderStatus,
                TotalAmount = existingOrder.TotalAmount,
                ShippingInformation = existingOrder.ShippingInformation,
                PaymentMethod = existingOrder.PaymentMethod,
                OrderDetails = existingOrder.OrderDetails,
                CreatedAt = existingOrder.CreatedAt,
            };

            return response;
        }

        public async Task<OrderDto> Create(OrderDto request)
        {
            string currentDate = DateTime.Now.ToString("ddMMyy");
            string lastUsedOrderNumber = await orderRepository.GetLastOrderNumberForDateAsync(currentDate);

            if (int.TryParse(lastUsedOrderNumber?.Substring(6, 4), out int lastOrderNumber))
            {
                lastOrderNumber++;
            }
            else
            {
                lastOrderNumber = 1000;
            }

            string orderNumber = $"{currentDate}{lastOrderNumber:D4}";

            var order = new Order
            {
                Id = request.Id,
                OrderNumber = orderNumber,
                OrderStatus = request.OrderStatus,
                TotalAmount = request.TotalAmount,
                ShippingInformation = request.ShippingInformation,
                PaymentMethod = request.PaymentMethod,
                OrderDetails = request.OrderDetails,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await orderRepository.CreateAsync(order);

            var response = new OrderDto
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

            return response;
        } 
    }
}

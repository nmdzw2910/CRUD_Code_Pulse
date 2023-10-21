using AutoMapper;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderRepository">IOrderRepository.</param>
        public OrderService(IOrderRepository orderRepository, ApplicationDbContext dbContext, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.dbContext = dbContext;
            this.mapper = mapper;
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
            if (dbContext.Entry(existingOrder.ShippingInformation).State != EntityState.Detached)
            {
                dbContext.Entry(existingOrder.ShippingInformation).State = EntityState.Detached;
            }

            mapper.Map(request, existingOrder);
            existingOrder.UpdatedAt = DateTime.Now;

            await orderRepository.UpdateAsync(existingOrder);
            return mapper.Map<OrderDto>(existingOrder);
        }

        public async Task<OrderDto> Create(OrderDto request)
        {
            var order = mapper.Map<Order>(request);

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

            order.OrderNumber = orderNumber;
            order.CreatedAt = DateTime.Now;
            order.OrderDetails = request.OrderDetails;

            await orderRepository.CreateAsync(order);

            var response = mapper.Map<OrderDto>(order);

            return response;
        } 
    }
}

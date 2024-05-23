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
        private readonly IOrderRepository _orderRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderRepository">IOrderRepository.</param>
        public OrderService(IOrderRepository orderRepository, ApplicationDbContext dbContext, IMapper mapper)
        {
            this._orderRepository = orderRepository;
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<OrderDto> Upsert(OrderDto orderDto)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(orderDto.Id);

            if (orderDto.Id != Guid.Empty && existingOrder != null)
            {
                return await Update(existingOrder, orderDto);
            }
            return await Create(orderDto);
        }

        public async Task<OrderDto> Update(Order existingOrder, OrderDto request)
        {
            if (_dbContext.Entry(existingOrder.ShippingInformation).State != EntityState.Detached)
            {
                _dbContext.Entry(existingOrder.ShippingInformation).State = EntityState.Detached;
            }

            _mapper.Map(request, existingOrder);
            existingOrder.UpdatedAt = DateTime.Now;

            await _orderRepository.UpdateAsync(existingOrder);
            return _mapper.Map<OrderDto>(existingOrder);
        }

        public async Task<OrderDto> Create(OrderDto request)
        {
            var order = _mapper.Map<Order>(request);

            string currentDate = DateTime.Now.ToString("ddMMyy");
            string? lastUsedOrderNumber = await _orderRepository.GetLastOrderNumberForDateAsync(currentDate);
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

            await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderDto>(order);
        } 
    }
}

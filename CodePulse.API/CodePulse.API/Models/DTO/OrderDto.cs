using CodePulse.API.Enums;
using CodePulse.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace CodePulse.API.Models.DTO
{
    public class OrderDto
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;
        public OrderStatus OrderStatus { get; set; }
        public float TotalAmount { get; set; }
        public ShippingInformation ShippingInformation { get; set; } = new ShippingInformation();
        public PaymentMethod PaymentInformation { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public DateTime CreatedAt { get; set; }
    }
}

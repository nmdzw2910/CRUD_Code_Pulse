using CodePulse.API.Enums;

namespace CodePulse.API.Models.Domain
{
    public class Order : BaseModel
    {
        public OrderStatus OrderStatus { get; set; }
        public string? OrderNumber { get; set; }
        public float TotalAmount { get; set; }
        public ShippingInformation ShippingInformation { get; set; } = new ShippingInformation();
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}

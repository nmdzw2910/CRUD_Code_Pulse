namespace CodePulse.API.Models.Domain
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}

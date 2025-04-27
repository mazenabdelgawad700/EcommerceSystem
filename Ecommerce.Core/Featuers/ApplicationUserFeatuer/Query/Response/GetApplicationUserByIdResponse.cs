namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Query.Response
{
    public class GetApplicationUserByIdResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime PlacedAt { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

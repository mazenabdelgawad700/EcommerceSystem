namespace Ecommerce.Domain.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
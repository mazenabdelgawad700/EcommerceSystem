namespace Ecommerce.Domain.Entities
{
    public class ProductInventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Inventory Inventory { get; set; }
    }
}

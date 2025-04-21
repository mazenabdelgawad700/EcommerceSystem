namespace Ecommerce.Domain.Entities
{
    public class ProductInventory
    {
        public ulong Id { get; set; }
        public ulong ProductId { get; set; }
        public int InventoryId { get; set; }
        public Product Product { get; set; }
        public Inventory Inventory { get; set; }
    }
}

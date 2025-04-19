namespace Ecommerce.Domain.Entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public DateTime LastDateProductAdded { get; set; }
        public ICollection<ProductInventory> ProductInventories { get; set; }
    }
}

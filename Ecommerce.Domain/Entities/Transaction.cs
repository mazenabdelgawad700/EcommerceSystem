using Ecommerce.Shared.Enums;

namespace Ecommerce.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public ulong OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public TransactionEnum Status { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}

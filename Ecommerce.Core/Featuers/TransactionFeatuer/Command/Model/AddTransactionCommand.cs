using Ecommerce.Shared.Base;
using Ecommerce.Shared.Enums;
using MediatR;

namespace Ecommerce.Core.Featuers.TransactionFeatuer.Command.Model
{
    public class AddTransactionCommand : IRequest<ReturnBase<bool>>
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public TransactionEnum Status { get; set; }
    }
}

using AutoMapper;
using Ecommerce.Core.Featuers.OrderItemFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.OrderItemMapping.Command
{
    public class AddOrderItemMapping : Profile
    {
        public AddOrderItemMapping()
        {
            CreateMap<AddOrderItemCommand, OrderItem>();
        }
    }
}

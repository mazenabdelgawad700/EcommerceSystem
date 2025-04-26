using AutoMapper;
using Ecommerce.Core.Featuers.OrderFeatuer.Ccommand.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.OrderMapping.Command
{
    public class AddOrderMapping : Profile
    {
        public AddOrderMapping()
        {
            CreateMap<AddOrderCommand, Order>();
        }
    }
}

using AutoMapper;
using Ecommerce.Core.Featuers.ProductInventoryFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ProductInventoryMapping.Command
{
    public class UpdateProductQuantityMapping : Profile
    {
        public UpdateProductQuantityMapping()
        {
            CreateMap<UpdateProductQuantityCommand, ProductInventory>();
        }
    }
}

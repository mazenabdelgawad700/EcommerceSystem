using AutoMapper;
using Ecommerce.Core.Featuers.CartFeatuer.Commnad.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.CartMapping.Command
{
    public class CreateCartMapping : Profile
    {
        public CreateCartMapping()
        {
            CreateMap<CreateCartCommand, Cart>();
        }
    }
}

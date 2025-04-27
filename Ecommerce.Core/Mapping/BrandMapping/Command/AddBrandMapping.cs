using AutoMapper;
using Ecommerce.Core.Featuers.BrandFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.BrandMapping.Command
{
    public class AddBrandMapping : Profile
    {
        public AddBrandMapping()
        {
            CreateMap<AddBrandCommand, Brand>();
        }
    }
}

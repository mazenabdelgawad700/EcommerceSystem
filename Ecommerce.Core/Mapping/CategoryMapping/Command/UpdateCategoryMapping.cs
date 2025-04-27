using AutoMapper;
using Ecommerce.Core.Featuers.CategoryFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.CategoryMapping.Command
{
    public class UpdateCategoryMapping : Profile
    {
        public UpdateCategoryMapping()
        {
            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}

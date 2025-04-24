using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ApplicationUserMapping.Command
{
    public class SaveRecentSearchResultMapping : Profile
    {
        public SaveRecentSearchResultMapping()
        {
            CreateMap<SaveRecentSearchResultCommand, RecentSearch>();
        }
    }
}

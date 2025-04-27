using AutoMapper;
using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Query.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ApplicationUserMapping.Query
{
    public class GetApplicationUserByIdMapping : Profile
    {
        public GetApplicationUserByIdMapping()
        {
            CreateMap<ApplicationUser, GetApplicationUserByIdResponse>();
            CreateMap<Order, OrderDto>();
        }
    }
}

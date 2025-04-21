using AutoMapper;
using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ApplicationUserMapping.Command
{
    public class UpdateApplicationUserProfile : Profile
    {
        public UpdateApplicationUserProfile()
        {
            CreateMap<UpdateApplicationUserCommand, ApplicationUser>();
        }
    }
}

using AutoMapper;
using Ecommerce.Core.Featuers.ReviewFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ReviewMapping.Command
{
    public class AddReviewMapping : Profile
    {
        public AddReviewMapping()
        {
            CreateMap<AddReviewCommand, Review>();
        }
    }
}

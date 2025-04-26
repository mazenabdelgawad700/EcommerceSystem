using AutoMapper;
using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.PaymentMethodMapping.Query
{
    public class GetPaymentMethodsMapping : Profile
    {
        public GetPaymentMethodsMapping()
        {
            CreateMap<PaymentMethod, GetPaymentMethodsResponse>();
        }
    }
}

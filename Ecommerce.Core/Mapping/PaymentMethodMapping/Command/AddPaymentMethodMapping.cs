using AutoMapper;
using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.PaymentMethodMapping.Command
{
    public class AddPaymentMethodMapping : Profile
    {
        public AddPaymentMethodMapping()
        {
            CreateMap<AddPaymentMethodCommand, PaymentMethod>();
        }
    }
}

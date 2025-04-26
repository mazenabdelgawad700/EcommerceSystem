using AutoMapper;
using Ecommerce.Core.Featuers.TransactionFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.TransactionMapping.Command
{
    public class AddTrasnactionMapping : Profile
    {
        public AddTrasnactionMapping()
        {
            CreateMap<AddTransactionCommand, Transaction>();
        }
    }
}

using AutoMapper;
using Ecommerce.Core.Featuers.TransactionFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.TransactionFeatuer.Command.Handler
{
    public class TransactionCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddTransactionCommand, ReturnBase<bool>>
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionCommandHandler(ITransactionService transactionService, IMapper mapper)
        {
            this._transactionService = transactionService;
            this._mapper = mapper;
        }


        public async Task<ReturnBase<bool>> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedResult = _mapper.Map<Transaction>(request);
                var addResult = await _transactionService.AddTransactionAsync(mappedResult);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}

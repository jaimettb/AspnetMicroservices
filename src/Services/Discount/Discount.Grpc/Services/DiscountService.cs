using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)) ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<CuponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var cupom = await _repository.GetDiscount(request.ProductName);
            
            if(cupom == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} not found"));
            }

            var cupomModel = _mapper.Map<CuponModel>(cupom);
            return cupomModel;
        }
    }
}

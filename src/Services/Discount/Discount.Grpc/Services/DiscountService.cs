using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper;
        }

        public override async Task<CuponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var cupom = await _repository.GetDiscount(request.ProductName);

            if (cupom == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} not found"));
            }

            _logger.LogInformation($"Discount is retrieved for ProductName:{request.ProductName}, Amont:{cupom.Amount}");

            var cupomModel = _mapper.Map<CuponModel>(cupom);
            return cupomModel;
        }

        public override async Task<CuponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var cupon = _mapper.Map<Coupon>(request.Cupon);

            await _repository.CreateDiscount(cupon);
            _logger.LogInformation($"Discount is sucessfully creates. ProductName: {cupon.ProductName}");

            var cuponModel = _mapper.Map<CuponModel>(cupon);
            return cuponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse()
            {
                Success = deleted
            };

            _logger.LogInformation($"Discount is deleted. ProductName: {request.ProductName}");

            return response;

        }
    }
}

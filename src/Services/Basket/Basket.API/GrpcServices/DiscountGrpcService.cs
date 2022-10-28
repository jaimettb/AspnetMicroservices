using Discount.Grpc.Protos;
using System;
using System.Threading.Tasks;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<CuponModel> GetDiscount(string productName)
        {
            var dicsountRequest = new GetDiscountRequest(){ProductName = productName};

            return await _discountProtoService.GetDiscountAsync(dicsountRequest);
        }
    }
}

using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController: ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
            this._discountGrpcService = discountGrpcService ?? throw new ArgumentException(nameof(discountGrpcService));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName).ConfigureAwait(false);
            return Ok(basket?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //Todo: Communicate with Discount.grpc
            //and calculate latest prices of product into shopping cart
            //Consume Discount Grpc

            foreach(var item in basket.Items)
            {
                var cupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= cupon.Amount;
            }

            return Ok(await _repository.UpdataBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName).ConfigureAwait(false);
            return Ok();
        }
    }
}

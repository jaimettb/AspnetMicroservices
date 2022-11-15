using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController: ControllerBase
    {
        private readonly ICatalogService catalogService;
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            this.catalogService = catalogService;
            this.basketService = basketService;
            this.orderService = orderService;
        }

        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {

        }
    }
}

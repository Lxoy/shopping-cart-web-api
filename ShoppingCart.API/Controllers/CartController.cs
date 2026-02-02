using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ViewModels.Requests;
using ShoppingCart.API.ViewModels.Responses;
using ShoppingCart.Services.Services;

namespace ShoppingCart.API.Controllers
{
    [Route("/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId:int}")]
        public IActionResult Get(int userId)
        {
            var cartDto = _cartService.GetByUserId(userId);

            var response = new CartResponse(
                cartDto.Items.Select(i =>
                new CartItemResponse(
                        i.ArticleId,
                        i.ArticleName,
                        i.ArticlePrice,
                        i.Quantity,
                        i.Total
                    )
                ).ToList(),
                cartDto.TotalPrice
                );

            return Ok(response);
        }

        [HttpPost("{userId:int}/items")]
        public IActionResult AddItem(int userId, [FromBody] AddCartItemRequest request)
        {
            _cartService.AddItem(userId, request.ArticleId, request.Quantity);
            return NoContent();
        }

        [HttpDelete("{userId:int}/items/{articleId:int}")]
        public IActionResult RemoveItem(int userId, int articleId)
        {
            _cartService.RemoveItem(userId, articleId);
            return NoContent();
        }

        [HttpDelete("{userId:int}/items")]
        public IActionResult RemoveAllItems(int userId)
        {
            _cartService.RemoveAllItems(userId);
            return NoContent();
        }
    }
}

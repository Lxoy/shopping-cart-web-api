using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ViewModels.Requests;
using ShoppingCart.API.ViewModels.Responses;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.API.Controllers
{

    [Route("/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet("{userId:int}")]
        public async Task<IActionResult> Get(int userId)
        {
            var cartDto = await _cartService.GetByUserId(userId);

            var response = new CartResponse(
                cartDto.Items.Select(i =>
                new CartItemResponse(
                        i.CartItemId,
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


        [HttpPost("{userId:int}/items/{articleId:int}")]
        public async Task<IActionResult> AddItem(int userId, int articleId)
        {
            await _cartService.AddItem(userId, articleId);
            return NoContent();
        }

        [HttpPatch("{userId:int}/items/{cartItemId:int}")]
        public async Task<IActionResult> UpdateItemQuantity(int userId, int cartItemId, [FromBody] ChangeCartItemQuantityRequest request)
        {
            await _cartService.UpdateItemQuantity(userId, cartItemId, request.Quantity);
            return NoContent();
        }

        [HttpDelete("{userId:int}/items/{cartItemId:int}")]
        public async Task<IActionResult> RemoveItem(int userId, int cartItemId)
        {
            await _cartService.RemoveItem(userId, cartItemId);
            return NoContent();
        }


        [HttpDelete("{userId:int}/items")]
        public async Task<IActionResult> RemoveAllItems(int userId)
        {
            await _cartService.RemoveAllItems(userId);
            return NoContent();
        }
    }
}

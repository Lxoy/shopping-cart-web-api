using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Dtos.Cart;
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

            var response = new CartResponseDto(
                cartDto.Items.Select(i => 
                new CartItemResponseDto(
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
    }
}

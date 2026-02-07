using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ViewModels.Requests;
using ShoppingCart.API.ViewModels.Responses;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.API.Controllers
{
    /// <summary>
    /// Provides HTTP endpoints for managing a user's shopping cart, including retrieving the cart, adding items,
    /// updating item quantities, and removing items.
    /// </summary>
    /// <remarks>This controller is intended to be used in an ASP.NET Core application as part of an
    /// e-commerce system. All actions require a valid user identifier and operate on the cart associated with that
    /// user. The controller returns standard HTTP status codes to indicate the result of each operation. Thread safety
    /// and transactional integrity are managed by the underlying service layer.</remarks>
    [Route("/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Retrieves the shopping cart for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose shopping cart is to be retrieved. Must be a valid user ID.</param>
        /// <returns>An <see cref="IActionResult"/> containing the user's shopping cart details if found; otherwise, a result
        /// indicating that the cart does not exist.</returns>
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

        /// <summary>
        /// Adds the specified article to the shopping cart for the given user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose cart will be updated. Must be a valid, existing user ID.</param>
        /// <param name="articleId">The unique identifier of the article to add to the user's cart. Must refer to a valid article.</param>
        /// <returns>A result indicating the outcome of the operation. Returns a 204 No Content response if the item is added
        /// successfully.</returns>
        [HttpPost("{userId:int}/items/{articleId:int}")]
        public async Task<IActionResult> AddItem(int userId, int articleId)
        {
            await _cartService.AddItem(userId, articleId);
            return NoContent();
        }

        /// <summary>
        /// Updates the quantity of a specific item in the user's shopping cart.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose cart is being modified.</param>
        /// <param name="cartItemId">The unique identifier of the cart item to update.</param>
        /// <param name="request">An object containing the new quantity for the cart item</param>
        /// <returns>A result indicating the outcome of the operation. Returns a 204 No Content response if the update is
        /// successful.</returns>
        [HttpPatch("{userId:int}/items/{cartItemId:int}")]
        public async Task<IActionResult> UpdateItemQuantity(int userId, int cartItemId, [FromBody] ChangeCartItemQuantityRequest request)
        {
            await _cartService.UpdateItemQuantity(userId, cartItemId, request.Quantity);
            return NoContent();
        }

        /// <summary>
        /// Removes the specified item from the user's shopping cart.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose cart item is to be removed.</param>
        /// <param name="cartItemId">The unique identifier of the cart item to remove from the user's cart.</param>
        /// <returns>An IActionResult that indicates the result of the remove operation. Returns a 204 No Content response if the
        /// item was successfully removed.</returns>
        [HttpDelete("{userId:int}/items/{cartItemId:int}")]
        public async Task<IActionResult> RemoveItem(int userId, int cartItemId)
        {
            await _cartService.RemoveItem(userId, cartItemId);
            return NoContent();
        }

        /// <summary>
        /// Removes all items from the cart for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose cart items will be removed. Must be a valid, existing user ID.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns a 204 No Content response if
        /// the items are successfully removed.</returns>
        [HttpDelete("{userId:int}/items")]
        public async Task<IActionResult> RemoveAllItems(int userId)
        {
            await _cartService.RemoveAllItems(userId);
            return NoContent();
        }
    }
}

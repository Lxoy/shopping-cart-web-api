using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ViewModels.Requests;
using ShoppingCart.API.ViewModels.Responses;
using ShoppingCart.Services.Interfaces;
using ShoppingCart.Services.Services;

namespace ShoppingCart.API.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to managing a user's shopping cart, including retrieving the cart, adding items,
    /// and removing items.
    /// </summary>
    /// <remarks>This controller provides endpoints for common shopping cart operations in an e-commerce
    /// application. All actions require a valid user identifier and operate on the cart associated with that user. The
    /// controller returns standard HTTP status codes to indicate the result of each operation. Thread safety and user
    /// authentication are expected to be managed by the broader application framework.</remarks>
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
        /// <param name="userId">The unique identifier of the user whose shopping cart is to be retrieved. Must be a positive integer.</param>
        /// <returns>An <see cref="IActionResult"/> containing the shopping cart details for the specified user. Returns a 200 OK
        /// response with the cart data if found; otherwise, the response may indicate that the cart is empty or not
        /// found.</returns>
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

        /// <summary>
        /// Adds a new item to the specified user's shopping cart.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose cart will be updated.</param>
        /// <param name="request">The details of the item to add to the cart, including the article identifier and quantity. Cannot be null.</param>
        /// <returns>An HTTP 204 No Content response if the item is added successfully.</returns>
        [HttpPost("{userId:int}/items")]
        public IActionResult AddItem(int userId, [FromBody] AddCartItemRequest request)
        {
            _cartService.AddItem(userId, request.ArticleId, request.Quantity);
            return NoContent();
        }

        /// <summary>
        /// Removes the specified item from the user's shopping cart.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose cart will be modified. Must be a valid user ID.</param>
        /// <param name="articleId">The unique identifier of the item to remove from the cart. Must correspond to an item currently in the
        /// user's cart.</param>
        /// <returns>An HTTP 204 No Content response if the item is successfully removed.</returns>
        [HttpDelete("{userId:int}/items/{articleId:int}")]
        public IActionResult RemoveItem(int userId, int articleId)
        {
            _cartService.RemoveItem(userId, articleId);
            return NoContent();
        }


        /// <summary>
        /// Removes all items from the shopping cart for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose cart items will be removed. Must be a valid user ID.</param>
        /// <returns>An HTTP 204 No Content response if the items were successfully removed.</returns>
        [HttpDelete("{userId:int}/items")]
        public IActionResult RemoveAllItems(int userId)
        {
            _cartService.RemoveAllItems(userId);
            return NoContent();
        }
    }
}

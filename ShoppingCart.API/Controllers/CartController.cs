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
        /// <remarks>This method is typically used to display the current contents of a user's shopping
        /// cart. The response includes item details and the total price. If the user does not have a cart, the response
        /// may indicate an empty cart.</remarks>
        /// <param name="userId">The unique identifier of the user whose cart is to be retrieved. Must be a positive integer.</param>
        /// <returns>An <see cref="IActionResult"/> containing the cart details for the specified user. Returns a 200 OK response
        /// with the cart data if found; otherwise, returns an appropriate error response.</returns>
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> Get(int userId)
        {
            var cartDto = await _cartService.GetByUserId(userId);

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
        /// <remarks>This method requires that the user exists and the item details are valid. If the
        /// operation succeeds, the cart is updated and no content is returned. The method is intended to be used in an
        /// HTTP POST request.</remarks>
        /// <param name="userId">The unique identifier of the user whose cart will be updated. Must be a valid user ID.</param>
        /// <param name="request">The details of the item to add to the cart, including the article identifier and quantity. Cannot be null.</param>
        /// <returns>An HTTP 204 No Content response if the item is successfully added to the cart.</returns>
        [HttpPost("{userId:int}/items")]
        public async Task<IActionResult> AddItem(int userId, [FromBody] AddCartItemRequest request)
        {
            await _cartService.AddItem(userId, request.ArticleId, request.Quantity);
            return NoContent();
        }

        /// <summary>
        /// Removes the specified item from the user's cart.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose cart will be modified. Must be a valid user ID.</param>
        /// <param name="articleId">The unique identifier of the item to remove from the cart. Must be a valid article ID present in the user's
        /// cart.</param>
        /// <returns>An <see cref="IActionResult"/> that indicates the result of the delete operation. Returns a 204 No Content
        /// response if the item is successfully removed.</returns>
        [HttpDelete("{userId:int}/items/{articleId:int}")]
        public async Task<IActionResult> RemoveItem(int userId, int articleId)
        {
            await _cartService.RemoveItem(userId, articleId);
            return NoContent();
        }

        /// <summary>
        /// Removes all items from the specified user's cart.
        /// </summary>
        /// <remarks>This operation deletes all items from the user's cart. If the user does not exist or
        /// has no items, the cart will be left empty. This endpoint is typically used to clear a cart before checkout
        /// or when the user wishes to start over.</remarks>
        /// <param name="userId">The unique identifier of the user whose cart items will be removed. Must be a valid user ID.</param>
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

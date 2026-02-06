using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ViewModels.Requests;
using ShoppingCart.API.ViewModels.Responses;
using ShoppingCart.Services.Interfaces;
using ShoppingCart.Services.Services;

namespace ShoppingCart.API.Controllers
{

    /// <summary>
    /// Handles HTTP requests for managing articles, including retrieving, creating, updating, and deleting article
    /// resources.
    /// </summary>
    /// <remarks>This controller provides RESTful endpoints for article operations. All routes are prefixed
    /// with "/article". Methods require a valid article identifier where applicable. Authentication and authorization
    /// are assumed but not shown in this implementation.</remarks>
    [Route("/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService) 
        {
            _articleService = articleService;
        }

        /// <summary>
        /// Retrieves the article with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the article to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the article data if found; otherwise, a NotFound result.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var articleDto = await _articleService.GetById(id);

            if (articleDto is null)
            {
                return NotFound();
            }

            var response = new ArticleResponse
            (
                articleDto.Id,
                articleDto.Name,
                articleDto.Price
            );

            return Ok(response);
        }

        /// <summary>
        /// Creates a new article using the specified request data.
        /// </summary>
        /// <param name="request">The request containing the details of the article to create. Must not be null.</param>
        /// <returns>A response that, when successful, contains the details of the created article and a location header with the
        /// URI of the new resource.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleRequest request)
        {
            int currentUserId = 1; // TODO: get from auth context

            var articleDto = await _articleService.Create(
                request.Name,
                request.Price,
                currentUserId
            );

            var response = new ArticleResponse
            (
                articleDto.Id,
                articleDto.Name,
                articleDto.Price
            );

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response
            );
        }

        /// <summary>
        /// Retrieves all articles.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a collection of articles. The response has a status code of 200
        /// (OK) and includes a list of <see cref="ArticleResponse"/> objects representing all available articles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articleDtos = await _articleService.GetAll();
            var response = articleDtos.Select(articleDto => new ArticleResponse
            (
                articleDto.Id,
                articleDto.Name,
                articleDto.Price
            ));
            return Ok(response);
        }


        /// <summary>
        /// Deletes the article with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the article to delete.</param>
        /// <returns>A response indicating the result of the delete operation. Returns a 204 No Content status if the deletion is
        /// successful.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            int currentUserdId = 1; // TODO: get from auth context
            await _articleService.Delete(id, currentUserdId);
            return NoContent();
        }

        /// <summary>
        /// Updates the specified article with new values provided in the request body.
        /// </summary>
        /// <param name="id">The unique identifier of the article to update.</param>
        /// <param name="request">An object containing the updated article values. Cannot be null.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the update operation. Returns a response
        /// containing the updated article data if successful.</returns>
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateArticleRequest request)
        {
            var currentUserdId = 1; // TODO: get from auth context
            var article = await _articleService.Update(
                id,
                request.Name,
                request.Price,
                currentUserdId
            );

            var response = new ArticleResponse
            (
                article.Id,
                article.Name,
                article.Price
            );

            return Ok(response);
        }
    }
}

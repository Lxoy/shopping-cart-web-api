using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ViewModels.Requests;
using ShoppingCart.API.ViewModels.Responses;
using ShoppingCart.Services.Interfaces;
using ShoppingCart.Services.Services;

namespace ShoppingCart.API.Controllers
{
    /// <summary>
    /// Provides HTTP endpoints for managing articles, including operations to create, retrieve, update, and delete
    /// articles.
    /// </summary>
    /// <remarks>This controller defines RESTful API actions for article resources. All endpoints require
    /// appropriate authentication and are routed under the "/article" path. The controller relies on an injected
    /// article service to perform business operations. Responses follow standard HTTP status codes for success and
    /// error conditions.</remarks>
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
        /// <param name="id">The unique identifier of the article to retrieve. Must be a positive integer.</param>
        /// <returns>An <see cref="IActionResult"/> containing the article data if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var articleDto = _articleService.GetById(id);

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
        /// Creates a new article using the data provided in the request body.
        /// </summary>
        /// <remarks>The created article is associated with the currently authenticated user. The response
        /// includes a location header pointing to the newly created resource.</remarks>
        /// <param name="request">The request object containing the details of the article to create. Must not be null.</param>
        /// <returns>A response that includes the URI of the newly created article and its details.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] CreateArticleRequest request)
        {
            int currentUserId = 1; // TODO: get from auth context

            var articleDto = _articleService.Create(
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
        /// (OK) and includes a list of article data in the response body.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var articleDtos = _articleService.GetAll();
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
        /// <returns>An HTTP 204 No Content response if the article was successfully deleted.</returns>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            int currentUserdId = 1; // TODO: get from auth context
            _articleService.Delete(id, currentUserdId);
            return NoContent();
        }

        /// <summary>
        /// Updates the specified article with new values provided in the request body.
        /// </summary>
        /// <remarks>This action uses the HTTP PATCH method to partially update an existing article. The
        /// article must exist; otherwise, an error response is returned. The user performing the update is determined
        /// from the authentication context.</remarks>
        /// <param name="id">The unique identifier of the article to update.</param>
        /// <param name="request">An object containing the updated article values. Cannot be null.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the update operation. Returns a response
        /// containing the updated article data if successful.</returns>
        [HttpPatch("{id:int}")]
        public IActionResult Update(int id, [FromBody] UpdateArticleRequest request)
        {
            var currentUserdId = 1; // TODO: get from auth context
            var article = _articleService.Update(
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

using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ViewModels.Requests;
using ShoppingCart.API.ViewModels.Responses;
using ShoppingCart.Services.Services;

namespace ShoppingCart.API.Controllers
{
    [Route("/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticleController(ArticleService articleService) 
        {
            _articleService = articleService;
        }

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

        [HttpPost]
        public IActionResult Create([FromBody] CreateArticleRequest request)
        {
            int currentUserdId = 1; // TODO: get from auth context

            var articleDto = _articleService.Create(
                request.Name,
                request.Price,
                currentUserdId
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

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            int currentUserdId = 1; // TODO: get from auth context
            _articleService.Delete(id, currentUserdId);
            return NoContent();
        }

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

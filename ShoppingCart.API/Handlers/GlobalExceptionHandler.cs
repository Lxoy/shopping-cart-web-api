using Microsoft.AspNetCore.Diagnostics;
using ShoppingCart.Services.Exceptions;

namespace ShoppingCart.API.Handlers
{
    public class GlobalExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            _logger.LogError(exception, exception?.Message);

            var statusCode = exception switch
            {
                InvalidCartArticlesException => StatusCodes.Status409Conflict,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            if (exception is InvalidCartArticlesException invalidCartEx)
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    errorCode = "INVALID_CART_ITEMS",
                    message = invalidCartEx.Message,
                    invalidCartItemIds = invalidCartEx.ArticleIds,
                    status = statusCode
                });

                return;
            }

            await context.Response.WriteAsJsonAsync(new
            {
                message = exception?.Message,
                status = statusCode
            });
        }
    }
}

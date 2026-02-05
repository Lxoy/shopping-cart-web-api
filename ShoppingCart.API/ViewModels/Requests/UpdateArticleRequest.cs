using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.ViewModels.Requests
{
    public record UpdateArticleRequest
    {
        [MinLength(1)]
        [MaxLength(100)]
        public string? Name { get; init; }

        [Range(0.01, double.MaxValue)]
        public decimal? Price { get; init; }
    }
}

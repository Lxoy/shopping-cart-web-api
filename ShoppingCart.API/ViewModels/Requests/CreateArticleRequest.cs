using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.ViewModels.Requests
{
    public record CreateArticleRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public required string Name { get; init; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; init; }
    }
}

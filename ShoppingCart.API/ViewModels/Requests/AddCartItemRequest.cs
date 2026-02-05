using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.ViewModels.Requests
{
    public record AddCartItemRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ArticleId { get; init; }

        [Required]
        [Range(1, 10)]
        public int Quantity { get; init; }
    }
}

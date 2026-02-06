using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.ViewModels.Requests
{
    public record ChangeCartItemQuantityRequest
    {
        [Required]
        public int Quantity { get; init; }
    }
}

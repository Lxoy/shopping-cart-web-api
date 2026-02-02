namespace ShoppingCart.API.ViewModels.Responses
{
    public record CartResponse(
        IReadOnlyCollection<CartItemResponse> Items,
        decimal TotalPrice
    );
}

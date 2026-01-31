namespace ShoppingCart.API.Dtos.Cart
{
    public record CartResponseDto(
        IReadOnlyCollection<CartItemResponseDto> Items,
        decimal TotalPrice
    );
}

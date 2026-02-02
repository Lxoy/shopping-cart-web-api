namespace ShoppingCart.Services.Dtos
{
    public record CartDto(int CartId, List<CartItemDto> Items, decimal TotalPrice);
}

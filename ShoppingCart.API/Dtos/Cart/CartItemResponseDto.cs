namespace ShoppingCart.API.Dtos.Cart
{
   public record CartItemResponseDto(int ArticleId, string Name, decimal Price, int Quantity, decimal Total);
}

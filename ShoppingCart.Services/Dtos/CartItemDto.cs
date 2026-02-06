namespace ShoppingCart.Services.Dtos
{
    public record CartItemDto(int CartItemId, int ArticleId, string ArticleName, decimal ArticlePrice, int Quantity, decimal Total);
}

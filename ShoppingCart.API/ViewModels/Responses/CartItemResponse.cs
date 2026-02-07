namespace ShoppingCart.API.ViewModels.Responses
{
   public record CartItemResponse(int Id, int ArticleId, string Name, decimal Price, int Quantity, decimal Total);
}

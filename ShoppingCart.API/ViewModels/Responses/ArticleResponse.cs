namespace ShoppingCart.API.ViewModels.Responses
{
    public record ArticleResponse(
        int Id,
        string Name,
        decimal Price
    );
}

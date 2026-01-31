namespace ShoppingCart.API.Dtos.Articles
{
    public record ArticleResponseDto(
        int Id,
        string Name,
        decimal Price
    );
}

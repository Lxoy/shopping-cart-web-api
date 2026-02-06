namespace ShoppingCart.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
        public int Quantity { get; set; }
    }
}

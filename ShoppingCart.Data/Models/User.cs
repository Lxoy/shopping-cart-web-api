namespace ShoppingCart.Data.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public Cart Cart { get; set; } = null!;
    }
}

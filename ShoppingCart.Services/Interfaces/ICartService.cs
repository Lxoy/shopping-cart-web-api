using ShoppingCart.Services.Dtos;

namespace ShoppingCart.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetByUserId(int userId);
        Task AddItem(int userId, int articleId);
        Task UpdateItemQuantity(int userId, int cartItemId, int quantity);
        Task RemoveItem(int userId, int cartItemId);
        Task RemoveAllItems(int userId);
    }
}

using ShoppingCart.Services.Dtos;

namespace ShoppingCart.Services.Interfaces
{
    public interface ICartService
    {
        Task AddItem(int userId, int articleId, int quantity);
        Task<CartDto> GetByUserId(int userId);
        Task RemoveItem(int userId, int articleId);
        Task RemoveAllItems(int userId);
    }
}

using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Data.Models;
using ShoppingCart.Services.Dtos;
using ShoppingCart.Services.Exceptions;
using ShoppingCart.Services.Interfaces;
namespace ShoppingCart.Services.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _dbContext;
        private const int MaxQuantity = 10;

        public CartService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartDto> GetByUserId(int userId)
        {
            var cart = await GetOrCreateCart(userId);

            var items = cart.CartItems.Select(ci =>
            {
                var total = ci.Quantity * ci.Article.Price;

                return new CartItemDto(
                    ci.Id,
                    ci.ArticleId,
                    ci.Article.Name,
                    ci.Article.Price,
                    ci.Quantity,
                    total
                    );
            }).ToList();

            var totalPrice = items.Sum(i => i.Total);

            return new CartDto(cart.Id, items, totalPrice);

        }

        public async Task AddItem(int userId, int articleId)
        {
            if (!await _dbContext.Articles.AnyAsync(a => a.Id == articleId && !a.IsDeleted))
                throw new KeyNotFoundException($"Article with id {articleId} not found.");

            var cart = await GetOrCreateCart(userId);
            ValidateCartArticles(cart);

            var item = cart.CartItems.FirstOrDefault(ci => ci.ArticleId == articleId);

            if (item is null)
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ArticleId = articleId,
                    Quantity = 1
                };

                cart.CartItems.Add(newItem);
                cart.ModifiedAt = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }

            else
            {
                throw new ArgumentException($"Item with id {articleId} is already in the cart.");
            }
        }

        public async Task UpdateItemQuantity(int userId, int cartItemId, int quantity)
        {
            var cart = await GetOrCreateCart(userId);
            
            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

            if (item == null)
                throw new KeyNotFoundException($"Item with id {cartItemId} not found.");

            if (!item.Article.IsDeleted)
                ValidateCartArticles(cart);

            if (quantity > MaxQuantity)
                throw new ArgumentException($"Maximum quantity per item is {MaxQuantity}.");

            if (quantity <= 0)
            {
                _dbContext.CartItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }

            cart.ModifiedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveItem(int userId, int cartItemId)
        {
            var cart = await GetOrCreateCart(userId);

            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

            if (item == null)
                throw new KeyNotFoundException($"Item with id {cartItemId} not found.");


            _dbContext.CartItems.Remove(item);
            cart.ModifiedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAllItems(int userId)
        {
            var cart = await GetOrCreateCart(userId);
            _dbContext.CartItems.RemoveRange(cart.CartItems);
            cart.ModifiedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Cart> GetOrCreateCart(int userId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Article)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId, CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow };
                _dbContext.Carts.Add(cart);

                await _dbContext.SaveChangesAsync();
            }

            return cart;
        }

        private static void ValidateCartArticles(Cart cart)
        {
            var invalidArticleIds = cart.CartItems
                .Where(ci => ci.Article.IsDeleted)
                .Select(ci => ci.ArticleId)
                .ToList();

            if (invalidArticleIds.Any())
            {
                throw new InvalidCartArticlesException(invalidArticleIds);
            }
        }
    }
}

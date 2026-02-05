using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Data.Models;
using ShoppingCart.Services.Dtos;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void AddItem(int userId, int articleId, int quantity)
        {

            if (!_dbContext.Articles.Any(a => a.Id == articleId && !a.IsDeleted))
            {
                throw new KeyNotFoundException($"Article with id {articleId} not found.");
            }

            if (quantity <= 0 || quantity > MaxQuantity)
                throw new ArgumentException($"Quantity must be between 1 and {MaxQuantity}.");

            var cart = GetOrCreateCart(userId);

            var item = cart.CartItems.FirstOrDefault(ci => ci.ArticleId == articleId);

            if (item == null)
            {
                cart.CartItems.Add(new CartItem
                {
                    ArticleId = articleId,
                    Quantity = quantity
                });
            }
            else
            {
                var newQuantity = item.Quantity + quantity;

                if (newQuantity > MaxQuantity)
                    throw new ArgumentException($"Maximum quantity per item is {MaxQuantity}.");

                item.Quantity = newQuantity;
            }

            cart.ModifiedAt = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }

        public CartDto GetByUserId(int userId)
        {
            var cart = GetOrCreateCart(userId);

            var items = cart.CartItems.Select(ci =>
            {
                var total = ci.Quantity * ci.Article.Price;

                return new CartItemDto(
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

        public void DecreaseItemQuantity(int userId, int articleId)
        {
            var cart = GetOrCreateCart(userId);

            var item = cart.CartItems
                .FirstOrDefault(ci => ci.ArticleId == articleId)
                ?? throw new KeyNotFoundException($"Item with id {articleId} not found.");

            if (item.Quantity <= 1)
                throw new InvalidOperationException("Quantity cannot be less than 1.");

            item.Quantity -= 1;

            if (item.Quantity <= 0)
            {
                _dbContext.CartItems.Remove(item);
            }

            cart.ModifiedAt = DateTime.UtcNow;

            _dbContext.SaveChanges();
        }

        public void RemoveItem(int userId, int articleId)
        {
            var cart = GetOrCreateCart(userId);

            var item = cart.CartItems
                .FirstOrDefault(ci => ci.ArticleId == articleId)
                ?? throw new KeyNotFoundException($"Item with id {articleId} not found.");

            _dbContext.CartItems.Remove(item);
            cart.ModifiedAt = DateTime.UtcNow;

            _dbContext.SaveChanges();
        }

        public void RemoveAllItems(int userId)
        {
            var cart = GetOrCreateCart(userId);
            _dbContext.CartItems.RemoveRange(cart.CartItems);
            cart.ModifiedAt = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }

        private Cart GetOrCreateCart(int userId)
        {
            var cart = _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Article)
                .FirstOrDefault(c => c.UserId == userId);

            var isNew = false;

            if (cart == null)
            {
                cart = new Cart { UserId = userId, CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow };
                _dbContext.Carts.Add(cart);
                isNew = true;
            }

            var removedItems = cart.CartItems
                .Where(ci => ci.Article == null || ci.Article.IsDeleted)
                .ToList();

            if (removedItems.Any())
            {
                _dbContext.CartItems.RemoveRange(removedItems);
            }

            if (isNew || removedItems.Any())
            {
                _dbContext.SaveChanges();
            }

            return cart;
        }
    }
}

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

        public CartService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddItem(int userId, int articleId, int quantity)
        {
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
                item.Quantity += quantity;
            }

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

        public void RemoveItem(int userId, int articleId)
        {
            var cart = GetOrCreateCart(userId);

            var item = cart.CartItems.FirstOrDefault(ci => ci.ArticleId == articleId) ?? throw new KeyNotFoundException($"Item with id {articleId} not found");

            if(item.Quantity > 1)
            {
                item.Quantity -= 1;
            }

            else
            {
                _dbContext.CartItems.Remove(item);
            }

            _dbContext.SaveChanges();
        }

        public void RemoveAllItems(int userId)
        {
            var cart = GetOrCreateCart(userId);
            _dbContext.CartItems.RemoveRange(cart.CartItems);
            _dbContext.SaveChanges();
        }

        private Cart GetOrCreateCart(int userId)
        {
            var cart = _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Article)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _dbContext.Carts.Add(cart);
                _dbContext.SaveChanges();
            }

            return cart;
        }
    }
}

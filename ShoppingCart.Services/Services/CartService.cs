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

        private Cart GetOrCreateCart(int userId)
        {
            var cart = _dbContext.Carts
                .Include(c => c.CartItems)
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

using ShoppingCart.Data.Models;
using ShoppingCart.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Interfaces
{
    internal interface ICartService
    {
        public void AddItem(int userId, int articleId, int quantity);
        public CartDto GetByUserId(int userId);
        public void RemoveItem(int userId, int articleId);
        public void RemoveAllItems(int userId);
    }
}

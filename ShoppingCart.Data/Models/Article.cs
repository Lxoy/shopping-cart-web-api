using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Models
{
    public class Article : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}

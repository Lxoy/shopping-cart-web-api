using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Dtos
{
    public record UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
    }
}

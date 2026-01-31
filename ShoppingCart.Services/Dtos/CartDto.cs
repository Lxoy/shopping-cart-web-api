using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Dtos
{
    public record CartDto(int CartId, List<CartItemDto> Items, decimal TotalPrice);
}

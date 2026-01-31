using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Dtos
{
    public record CartItemDto(int ArticleId, string ArticleName, decimal ArticlePrice, int Quantity, decimal Total);
}

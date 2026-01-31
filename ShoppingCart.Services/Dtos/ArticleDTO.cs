using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Dtos
{
    public record ArticleDto(int Id, string Name, decimal Price);
}

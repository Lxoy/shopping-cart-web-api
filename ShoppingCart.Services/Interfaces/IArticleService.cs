using ShoppingCart.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Interfaces
{
    internal interface IArticleService
    {
        ArticleDto GetById(int id);
        IEnumerable<ArticleDto> GetAll();
        ArticleDto Create(string name, decimal price, int createdByUserId);
        void Delete(int id, int deletedByUserId);
        ArticleDto Update(int id, string? name, decimal? price, int modifiedByUserId);
    }
}

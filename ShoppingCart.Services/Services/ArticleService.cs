using ShoppingCart.Data;
using ShoppingCart.Data.Models;
using ShoppingCart.Services.Dtos;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _dbContext;

        public ArticleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ArticleDto Create(string name, decimal price, int createdByUserId)
        {
            var article = new Article
            {
                Name = name,
                Price = price,
                CreatedByUserId = createdByUserId,
                ModifiedByUserId = createdByUserId
            };

            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();
            
            return new ArticleDto(
                article.Id,
                article.Name,
                article.Price
                );
        }

        public IEnumerable<ArticleDto> GetAll()
        {
            return _dbContext.Articles.Select(a => new ArticleDto(
                a.Id,
                a.Name,
                a.Price
                )).ToList();
        }

        public ArticleDto GetById(int id)
        {
            var article = _dbContext.Articles.Find(id) ?? throw new KeyNotFoundException($"Article with id {id} not found.");

            return new ArticleDto(
                article.Id,
                article.Name,
                article.Price
                );
        }
    }
}

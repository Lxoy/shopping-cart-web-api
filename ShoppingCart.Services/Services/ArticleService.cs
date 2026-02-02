using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Data.Models;
using ShoppingCart.Services.Dtos;
using ShoppingCart.Services.Interfaces;

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
                ModifiedByUserId = createdByUserId,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();
            
            return new ArticleDto(
                article.Id,
                article.Name,
                article.Price
                );
        }

        public void Delete(int id, int deletedByUserId)
        {
            var article = _dbContext.Articles.FirstOrDefault(a => a.Id == id);

            if (article == null)
            {
                throw new KeyNotFoundException($"Article with id {id} not found.");
            }

            article.IsDeleted = true;
            article.ModifiedAt = DateTime.UtcNow;
            article.ModifiedByUserId = deletedByUserId;

            _dbContext.SaveChanges();
        }

        public IEnumerable<ArticleDto> GetAll()
        {
            return _dbContext.Articles
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .Select(a => new ArticleDto(
                    a.Id,
                    a.Name,
                    a.Price
                ))
                .ToList();
        }

        public ArticleDto GetById(int id)
        {
            var article = _dbContext.Articles
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == id && !a.IsDeleted) 
                ?? throw new KeyNotFoundException($"Article with id {id} not found.");

            return new ArticleDto(
                article.Id,
                article.Name,
                article.Price
                );
        }

        public ArticleDto Update(int id, string? name, decimal? price, int modifiedByUserId)
        {
            if (name is null && price is null )
            {
                throw new ArgumentException("At least one field (name or price) must be provided for update.");
            }

            var article = _dbContext.Articles.FirstOrDefault(a => a.Id == id && !a.IsDeleted) ?? throw new KeyNotFoundException($"Article with id {id} not found."); ;

            if(name is not null)
            {
                article.Name = name;
            }

            if (price is not null)
            {
                article.Price = price.Value;
            }

            article.ModifiedAt = DateTime.UtcNow;
            article.ModifiedByUserId = modifiedByUserId;

            _dbContext.SaveChanges();

            return new ArticleDto(article.Id, article.Name, article.Price);
        }
    }
}

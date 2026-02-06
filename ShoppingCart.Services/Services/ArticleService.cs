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

        public async Task<ArticleDto> Create(string name, decimal price, int createdByUserId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Article name is required.");
            }

            if (price < 0.1m)
            {
                throw new ArgumentException("Price must be at least 0.1.");
            }

            var article = new Article
            {
                Name = name,
                Price = price,
                CreatedByUserId = createdByUserId,
                ModifiedByUserId = createdByUserId,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            await  _dbContext.Articles.AddAsync(article);
            await _dbContext.SaveChangesAsync();
            
            return new ArticleDto(
                article.Id,
                article.Name,
                article.Price
                );
        }

        public async Task Delete(int id, int deletedByUserId)
        {
            var article = await _dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                throw new KeyNotFoundException($"Article with id {id} not found.");
            }

            article.IsDeleted = true;
            article.ModifiedAt = DateTime.UtcNow;
            article.ModifiedByUserId = deletedByUserId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ArticleDto>> GetAll()
        {
            return await _dbContext.Articles
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .Select(a => new ArticleDto(
                    a.Id,
                    a.Name,
                    a.Price
                ))
                .ToListAsync();
        }

        public async Task<ArticleDto> GetById(int id)
        {
            var article = await _dbContext.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted) 
                ?? throw new KeyNotFoundException($"Article with id {id} not found.");

            return new ArticleDto(
                article.Id,
                article.Name,
                article.Price
                );
        }

        public async Task<ArticleDto> Update(int id, string? name, decimal? price, int modifiedByUserId)
        {
            if (name is not null && string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Article name cannot be empty.");
            }

            if (price is not null && price < 0.1m)
            {
                throw new ArgumentException("Price must be at least 0.1.");
            }

            var article = await _dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted) ?? throw new KeyNotFoundException($"Article with id {id} not found.");

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

            await _dbContext.SaveChangesAsync();

            return new ArticleDto(article.Id, article.Name, article.Price);
        }
    }
}

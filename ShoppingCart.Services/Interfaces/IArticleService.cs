using ShoppingCart.Services.Dtos;

namespace ShoppingCart.Services.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDto> GetById(int id);
        Task<IEnumerable<ArticleDto>> GetAll();
        Task<ArticleDto> Create(string name, decimal price, int createdByUserId);
        Task Delete(int id, int deletedByUserId);
        Task<ArticleDto> Update(int id, string? name, decimal? price, int modifiedByUserId);
    }
}

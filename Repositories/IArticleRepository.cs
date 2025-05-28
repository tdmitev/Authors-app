using SportScore2.Api.Models;

namespace SportScore2.Api.Repositories;

public interface IArticleRepository
{
    Task<bool> AuthorExistsAsync(Guid authorId);
    Task<IEnumerable<Article>> GetArticlesByAuthorAsync(Guid authorId, int pageNumber, int pageSize);
    Task<Article?> GetByIdAsync(Guid authorId, Guid articleId);
    Task AddAsync(Article article);
    Task UpdateAsync(Article article);
    Task DeleteAsync(Guid authorId, Guid articleId);
}
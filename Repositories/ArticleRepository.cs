using SportScore2.Api.Models;

namespace SportScore2.Api.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly List<Article> _articles = new();

    public Task<bool> AuthorExistsAsync(Guid authorId) =>
        Task.FromResult(true);

    public Task<IEnumerable<Article>> GetArticlesByAuthorAsync(
        Guid authorId, int pageNumber, int pageSize) =>
        Task.FromResult(_articles
            .Where(a => a.AuthorId == authorId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsEnumerable());

    public Task<Article?> GetByIdAsync(Guid authorId, Guid articleId) =>
        Task.FromResult(_articles
            .FirstOrDefault(a => a.AuthorId == authorId && a.Id == articleId));

    public Task AddAsync(Article article)
    {
        _articles.Add(article);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Article article)
    {
        // In-memory
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid authorId, Guid articleId)
    {
        var art = _articles.FirstOrDefault(a =>
            a.AuthorId == authorId && a.Id == articleId);
        if (art != null) _articles.Remove(art);
        return Task.CompletedTask;
    }
}
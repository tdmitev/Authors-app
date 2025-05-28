using Microsoft.EntityFrameworkCore;
using SportScore2.Api.Data;
using SportScore2.Api.Models;

namespace SportScore2.Api.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _ctx;

    public ArticleRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<bool> AuthorExistsAsync(Guid authorId) =>
        _ctx.Authors.AnyAsync(a => a.Id == authorId);

    public async Task<IEnumerable<Article>> GetArticlesByAuthorAsync(
        Guid authorId, int pageNumber, int pageSize) =>
        await _ctx.Articles
            .Where(a => a.AuthorId == authorId)
            .OrderBy(a => a.Published)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Article?> GetByIdAsync(Guid authorId, Guid articleId) =>
        await _ctx.Articles
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AuthorId == authorId && a.Id == articleId);

    public async Task AddAsync(Article article)
    {
        _ctx.Articles.Add(article);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Article article)
    {
        _ctx.Articles.Update(article);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid authorId, Guid articleId)
    {
        var art = await _ctx.Articles
            .FirstOrDefaultAsync(a => a.AuthorId == authorId && a.Id == articleId);
        if (art != null)
        {
            _ctx.Articles.Remove(art);
            await _ctx.SaveChangesAsync();
        }
    }
}
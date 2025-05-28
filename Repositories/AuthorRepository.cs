using Microsoft.EntityFrameworkCore;
using SportScore2.Api.Data;
using SportScore2.Api.Models;

namespace SportScore2.Api.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _ctx;

    public AuthorRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Author>> GetAllAsync() =>
        await _ctx.Authors.AsNoTracking().ToListAsync();

    public async Task<Author?> GetByIdAsync(Guid authorId) =>
        await _ctx.Authors.FindAsync(authorId);

    public async Task AddAsync(Author author)
    {
        _ctx.Authors.Add(author);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        // _ctx.Authors.Update(author); // по желание
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid authorId)
    {
        var entity = await _ctx.Authors.FindAsync(authorId);
        if (entity != null)
        {
            _ctx.Authors.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
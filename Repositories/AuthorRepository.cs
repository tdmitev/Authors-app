using SportScore2.Api.Models;

namespace SportScore2.Api.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly List<Author> _authors = new();

    public Task<IEnumerable<Author>> GetAllAsync() =>
        Task.FromResult(_authors.AsEnumerable());

    public Task<Author?> GetByIdAsync(Guid authorId) =>
        Task.FromResult(_authors.FirstOrDefault(a => a.Id == authorId));

    public Task AddAsync(Author author)
    {
        _authors.Add(author);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Author author)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid authorId)
    {
        var a = _authors.FirstOrDefault(x => x.Id == authorId);
        if (a != null) _authors.Remove(a);
        return Task.CompletedTask;
    }
}
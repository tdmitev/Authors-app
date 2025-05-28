using SportScore2.Api.Models;

namespace SportScore2.Api.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(Guid authorId);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(Guid authorId);
}
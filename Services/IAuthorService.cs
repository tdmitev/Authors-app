using SportScore2.Api.DTO;

namespace SportScore2.Api.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<AuthorDto> GetByIdAsync(Guid authorId);
    Task<AuthorDto> CreateAsync(CreateAuthorDto dto);
    Task<AuthorDto> UpdateAsync(Guid authorId, UpdateAuthorDto dto);
    Task DeleteAsync(Guid authorId);
}
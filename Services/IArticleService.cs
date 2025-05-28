using SportScore2.Api.DTO;

namespace SportScore2.Api.Services;

public delegate void PageFetchedDelegate(int pageNumber);

public interface IArticleService
{
    event PageFetchedDelegate PageFetched;
    Task<IEnumerable<ArticleDto>> GetArticlesByAuthorParallelAsync(
        Guid authorId,
        IEnumerable<int> pages,
        int pageSize);

    Task<ArticleDto> GetByIdAsync(Guid authorId, Guid articleId);
    Task<ArticleDto> CreateAsync(Guid authorId, CreateArticleDto dto);
    Task<ArticleDto> UpdateAsync(Guid authorId, Guid articleId, UpdateArticleDto dto);
    Task DeleteAsync(Guid authorId, Guid articleId);
}
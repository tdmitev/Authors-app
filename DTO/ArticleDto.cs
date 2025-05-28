namespace SportScore2.Api.DTO;

public record ArticleDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime Published { get; init; }
    public string Summary { get; init; } = string.Empty;
}
namespace SportScore2.Api.Models;

public class Article
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Published { get; set; }
    public string Content { get; set; } = string.Empty;
}
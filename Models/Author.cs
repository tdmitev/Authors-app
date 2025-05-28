namespace SportScore2.Api.Models;

public class Author
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Навигационно свойство за EF Core:
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
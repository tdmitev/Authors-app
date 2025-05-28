using Microsoft.AspNetCore.Mvc;
using SportScore2.Api.DTO;
using SportScore2.Api.Services;

namespace SportScore2.Api.Controllers;

[ApiController]
[Route("api/authors/{authorId:guid}/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _svc;

    public ArticlesController(IArticleService svc)
    {
        _svc = svc;
        _svc.PageFetched += p => Console.WriteLine($"Page {p} fetched.");
    }

    [HttpGet("parallel")]
    public async Task<IActionResult> GetParallel(
        Guid authorId,
        [FromQuery] int[] pages,
        [FromQuery] int pageSize = 10) =>
        Ok(await _svc.GetArticlesByAuthorParallelAsync(authorId, pages, pageSize));

    [HttpGet("{articleId:guid}")]
    public async Task<IActionResult> GetById(
        Guid authorId, Guid articleId) =>
        Ok(await _svc.GetByIdAsync(authorId, articleId));

    [HttpPost]
    public async Task<IActionResult> Create(
        Guid authorId, [FromBody] CreateArticleDto dto)
    {
        var created = await _svc.CreateAsync(authorId, dto);
        return CreatedAtAction(
            nameof(GetById),
            new { authorId, articleId = created.Id },
            created);
    }

    [HttpPut("{articleId:guid}")]
    public async Task<IActionResult> Update(
        Guid authorId, Guid articleId, [FromBody] UpdateArticleDto dto) =>
        Ok(await _svc.UpdateAsync(authorId, articleId, dto));

    [HttpDelete("{articleId:guid}")]
    public async Task<IActionResult> Delete(
        Guid authorId, Guid articleId)
    {
        await _svc.DeleteAsync(authorId, articleId);
        return NoContent();
    }
}
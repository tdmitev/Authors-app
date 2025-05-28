using Microsoft.AspNetCore.Mvc;
using SportScore2.Api.DTO;
using SportScore2.Api.Services;

namespace SportScore2.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _svc;

    public AuthorsController(IAuthorService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _svc.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(await _svc.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
    {
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id, [FromBody] UpdateAuthorDto dto) =>
        Ok(await _svc.UpdateAsync(id, dto));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }
}
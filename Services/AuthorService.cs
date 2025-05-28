using AutoMapper;
using SportScore2.Api.DTO;
using SportScore2.Api.Exception;
using SportScore2.Api.Models;
using SportScore2.Api.Repositories;

namespace SportScore2.Api.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repo;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync() =>
        (await _repo.GetAllAsync())
        .Select(a => _mapper.Map<AuthorDto>(a));

    public async Task<AuthorDto> GetByIdAsync(Guid authorId)
    {
        var entity = await _repo.GetByIdAsync(authorId)
                     ?? throw new NotFoundException($"Author {authorId} not found.");
        return _mapper.Map<AuthorDto>(entity);
    }

    public async Task<AuthorDto> CreateAsync(CreateAuthorDto dto)
    {
        var model = _mapper.Map<Author>(dto);
        await _repo.AddAsync(model);
        return _mapper.Map<AuthorDto>(model);
    }

    public async Task<AuthorDto> UpdateAsync(Guid authorId, UpdateAuthorDto dto)
    {
        var existing = await _repo.GetByIdAsync(authorId)
                       ?? throw new NotFoundException($"Author {authorId} not found.");
        _mapper.Map(dto, existing);
        await _repo.UpdateAsync(existing);
        return _mapper.Map<AuthorDto>(existing);
    }

    public async Task DeleteAsync(Guid authorId)
    {
        var existing = await _repo.GetByIdAsync(authorId)
                       ?? throw new NotFoundException($"Author {authorId} not found.");
        await _repo.DeleteAsync(authorId);
    }
}
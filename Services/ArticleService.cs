using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SportScore2.Api.DTO;
using SportScore2.Api.Exception;
using SportScore2.Api.Models;
using SportScore2.Api.Repositories;

namespace SportScore2.Api.Services;

 public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repo;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly int _cacheDuration;
        public event PageFetchedDelegate? PageFetched;

        public ArticleService(
            IArticleRepository repo,
            IMemoryCache cache,
            IMapper mapper,
            IConfiguration config)
        { 
            _repo = repo;
            _cache = cache;
            _mapper = mapper;
            _cacheDuration = config.GetValue<int>("CacheSettings:AuthorArticlesDurationMinutes");
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesByAuthorParallelAsync(
            Guid authorId,
            IEnumerable<int> pages,
            int pageSize)
        {
            if (!await _repo.AuthorExistsAsync(authorId))
                throw new NotFoundException($"Author {authorId} not found.");

            var tasks = pages.Select(async p =>
            {
                string key = $"A:{authorId}:{p}:{pageSize}";
                if (!_cache.TryGetValue(key, out List<ArticleDto> dtos))
                {
                    var models = await _repo.GetArticlesByAuthorAsync(authorId, p, pageSize);
                    dtos = models.Select(m => _mapper.Map<ArticleDto>(m)).ToList();
                    _cache.Set(key, dtos, TimeSpan.FromMinutes(_cacheDuration));
                }
                PageFetched?.Invoke(p);
                return (IEnumerable<ArticleDto>)dtos;
            });

            var results = await Task.WhenAll(tasks);
            return results.SelectMany(x => x);
        }

        public async Task<ArticleDto> GetByIdAsync(Guid authorId, Guid articleId)
        {
            var model = await _repo.GetByIdAsync(authorId, articleId)
                ?? throw new NotFoundException($"Article {articleId} not found.");
            return _mapper.Map<ArticleDto>(model);
        }

        public async Task<ArticleDto> CreateAsync(Guid authorId, CreateArticleDto dto)
        {
            if (!await _repo.AuthorExistsAsync(authorId))
                throw new NotFoundException($"Author {authorId} not found.");

            var model = _mapper.Map<Article>(dto);
            model.AuthorId = authorId;
            await _repo.AddAsync(model);
            
            _cache.Remove($"A:{authorId}:1:10");

            return _mapper.Map<ArticleDto>(model);
        }

        public async Task<ArticleDto> UpdateAsync(
            Guid authorId, Guid articleId, UpdateArticleDto dto)
        {
            var existing = await _repo.GetByIdAsync(authorId, articleId)
                           ?? throw new NotFoundException($"Article {articleId} not found.");
            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            
            _cache.Remove($"A:{authorId}:1:10");

            return _mapper.Map<ArticleDto>(existing);
        }

        public async Task DeleteAsync(Guid authorId, Guid articleId)
        {
            var existing = await _repo.GetByIdAsync(authorId, articleId)
                           ?? throw new NotFoundException($"Article {articleId} not found.");
            await _repo.DeleteAsync(authorId, articleId);
            
            _cache.Remove($"A:{authorId}:1:10");
        }
    }
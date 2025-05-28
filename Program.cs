using SportScore2.Api.Exception;
using SportScore2.Api.Filters;
using SportScore2.Api.Mappers;
using SportScore2.Api.Repositories;
using SportScore2.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Suppress default ModelState invalid filter
builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Memory cache
builder.Services.AddMemoryCache();

// DI: репозитории и услуги
builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();
builder.Services.AddSingleton<IArticleRepository, ArticleRepository>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();
builder.Services.AddSingleton<IArticleService, ArticleService>();

// Валидационен филтър
builder.Services.AddSingleton<ValidationFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

var app = builder.Build();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Глобални exception-и
app.UseMiddleware<ExceptionMiddleware>();

// Мапваме само контролерите
app.MapControllers();

app.Run();
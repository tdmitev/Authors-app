using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SportScore2.Api.Data;
using SportScore2.Api.Exception;
using SportScore2.Api.Filters;
using SportScore2.Api.Mappers;
using SportScore2.Api.Repositories;
using SportScore2.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Suppress default ModelState invalid filter
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// 2. Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// 4. Memory cache
builder.Services.AddMemoryCache();

// 5. EF Core DbContext (MySQL)
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

// 6. DI: репозитории и услуги като Scoped
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IArticleService, ArticleService>();

// 7. Validation filter
builder.Services.AddSingleton<ValidationFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

var app = builder.Build();

// ➤ Serve wwwroot/index.html
app.UseDefaultFiles();    // търси index.html
app.UseStaticFiles();     // serve-ва файловете от wwwroot

// 8. Swagger UI на алтернативен маршрут (/swagger)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SportScore2 API V1");
    c.RoutePrefix = "swagger"; // вече няма да е на корена
});

// 9. Глобални exception-и
app.UseMiddleware<ExceptionMiddleware>();

// 10. Map controllers
app.MapControllers();

app.Run();
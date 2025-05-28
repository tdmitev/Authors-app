using Microsoft.EntityFrameworkCore;
using SportScore2.Api.Models;

namespace SportScore2.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relations, ако искаш cascade delete:
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Articles)
                .WithOne()
                .HasForeignKey(ar => ar.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
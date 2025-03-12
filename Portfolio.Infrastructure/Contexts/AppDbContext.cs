using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(conString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .HasMany(art => art.Authors)
            .WithMany(aut => aut.Articles)
            .UsingEntity<ArticleAuthor>();

        modelBuilder.Entity<Article>()
            .HasMany(art => art.Categories)
            .WithMany(cat => cat.Articles)
            .UsingEntity<ArticleCategory>();
    }
}
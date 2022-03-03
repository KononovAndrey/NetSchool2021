namespace CodeFirstExample.Data;

using CodeFirstExample.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class MainDbContext : DbContext
{
    DbSet<Article> Articles { get; set; }
    DbSet<Comment> Comments { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

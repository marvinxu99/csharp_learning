using Microsoft.EntityFrameworkCore;

namespace EFCorePostgreSQL;

internal class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=csharp_dbtest;Username=winter;Password=123456");
    }

}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCorePostgreSQL.Models;

internal class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=csharp_dbtest;Username=winter;Password=123456");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)  // Set base path to the output directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Ensure it's not optional
            .Build();

        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

}

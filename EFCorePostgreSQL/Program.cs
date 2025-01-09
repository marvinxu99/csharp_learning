using Microsoft.EntityFrameworkCore;

namespace EFCorePostgreSQL;

internal class Program
{
    static async Task Main()
    {
        using var context = new AppDbContext();

        // Add initial products
        /*
        context.Products.AddRange(
            new Product { Name = "Laptop", Price = 1200.50M, Stock = 10 },
            new Product { Name = "Phone", Price = 800.75M, Stock = 20 }
        );
        await context.SaveChangesAsync();
        Console.WriteLine("Products added to the database.");
        */

        //Query the Database
        var products = await context.Products
            .Where(p => p.Stock > 0)
            .ToListAsync();

        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id} - {product.Name}: {product.Price:C} (Stock: {product.Stock})");
        }

        // Filterimg
        var expensiveProducts = context.Products.
            Where(p => p.Price > 1000)
            .ToList();
        foreach (var product in expensiveProducts)
        {
            Console.WriteLine($"{product.Id} - {product.Name}: {product.Price:C} (Stock: {product.Stock})");
        }

        // Select a specific field
        var productNames = await context.Products
            .Select(p => p.Name)
            .ToListAsync();
        foreach (var name in productNames)
        {
            Console.WriteLine($"{name}");
        }
    }
}

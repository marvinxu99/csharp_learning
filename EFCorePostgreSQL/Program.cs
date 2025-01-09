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
        var expensiveProducts = await context.Products.
            Where(p => p.Price > 1000)
            .ToListAsync();
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

        // Update data
        /*
        var prod = context.Products.FirstOrDefault(p => p.Name == "Laptop");
        if (prod != null)
        {
            prod.Stock -= 1; // Reduce stock
            context.SaveChanges();
        }
        */

        // Delete data
        /*
        var prodToRemove = context.Products.FirstOrDefault(p => p.Name == "Laptop");
        if (prodToRemove != null)
        {
            context.Products.Remove(prodToRemove);
            context.SaveChanges();
        }
        */

        // Group By
        var groupedProducts = context.Products
            .GroupBy(p => p.Stock)
            .Select(g => new
            {
                StockLevel = g.Key,
                Products = g.ToList()
            })
            .ToList();

        foreach (var items in groupedProducts)
        {
            Console.WriteLine($"{items.StockLevel}");
            foreach (var item in items.Products)
            {
                Console.WriteLine(item.Name);
            }

        }

        // Joins
        // Add Categories
        /*
        var electronics = new Category { Name = "Electronics" };
        var furniture = new Category { Name = "Furniture" };
        context.Categories.AddRange(electronics, furniture);
        context.SaveChanges();

        // Add Products
        context.Products.Add(new Product { Name = "Laptop", Price = 1200.50M, Stock = 10, CategoryId = electronics.Id });
        context.Products.Add(new Product { Name = "Sofa", Price = 800.00M, Stock = 5, CategoryId = furniture.Id });
        context.SaveChanges();
        */
        var productsWithCategories = await context.Products
            .Include(p => p.Category)
            .Select(p => new
            {
                ProductName = p.Name,
                CategoryName = p.Category.Name
            })
            .ToListAsync();

        foreach (var item in productsWithCategories)
        {
            Console.WriteLine($"{item.ProductName} - {item.CategoryName}");
        }

        // Pagination
        // Fetch records in chunks:
        int pageNumber = 1;
        int pageSize = 10;

        var paginatedProducts = await context.Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    }
}

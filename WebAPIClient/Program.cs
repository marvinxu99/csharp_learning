// https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient

using System.Net.Http.Headers;
using System.Text.Json;

namespace WebAPIClient;

internal class Program
{
    static async Task Main()
    {
        using HttpClient client = new();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        var repositories = await ProcessRepositoriesAsync(client);

        foreach (var repo in repositories)
        {
            Console.WriteLine($"Name: {repo.Name}");
            Console.WriteLine($"HomePage: {repo.Homepage}");
            Console.WriteLine($"GutHub: {repo.GitHubHomeUrl}");
            Console.WriteLine($"Description: {repo.Description}");
            Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
            Console.WriteLine($"Last push: {repo.LastPush}");
            Console.WriteLine();
        }

    }
    private static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
    {
        // Return string
        //var json = await client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

        await using Stream stream =
            await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
        var repositories =
            await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

        return repositories ?? [];
    }
}

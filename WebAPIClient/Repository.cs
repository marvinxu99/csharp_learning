namespace WebAPIClient;

//public record class Repository(string name);

using System.Text.Json.Serialization;

// Changes the name of the name property to Name.
// Adds the JsonPropertyNameAttribute to specify how this property appears in the JSON.
public record class Repository(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("html_url")] Uri GitHubHomeUrl,
    [property: JsonPropertyName("homepage")] Uri Homepage,
    [property: JsonPropertyName("watchers")] int Watchers,
    [property: JsonPropertyName("pushed_at")] DateTime LastPushUtc)
{
    public DateTime LastPush => LastPushUtc.ToLocalTime();
}



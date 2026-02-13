using System.Text.Json;
using Server.Infrastructure.ExectionHandling.Localization;

public class ErrorLocalizer : IErrorLocalizer
{
    // cache -> dosyayı her seferinde okumayalım (çok önemli)
    private static readonly Dictionary<string, Dictionary<string, string>> _cache = new();

    public string GetDescription(string key, string language)
    {
        var dict = LoadLanguage(language);

        return dict.GetValueOrDefault(key, key);
    }

    private static Dictionary<string, string> LoadLanguage(string language)
    {
        if (_cache.TryGetValue(language, out var cached))
            return cached;

        var fileName = $"errors-{language}.json";
        var fullPath = Path.Combine("Resources", fileName);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException(
                $"Localization file not found. Expected at: {fullPath}\n" +
                $"BaseDirectory: {AppContext.BaseDirectory}\n" +
                $"CurrentDirectory: {Directory.GetCurrentDirectory()}");
        }

        var json = File.ReadAllText(fullPath);

        var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                   ?? new Dictionary<string, string>();

        _cache[language] = dict;
        return dict;
    }
}
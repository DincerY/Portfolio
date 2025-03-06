using System.Text.Json;
using StackExchange.Redis;

namespace Portfolio.Application.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IDatabase database)
    {
        _database = database;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var val = await _database.StringGetAsync(key);
        return val.HasValue ? JsonSerializer.Deserialize<T>(val) : default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var serializedVal = JsonSerializer.Serialize(value,new JsonSerializerOptions(){Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
        await _database.StringSetAsync(key,serializedVal,expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}
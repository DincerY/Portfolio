
using System.Collections.Concurrent;
using System.Text.Json;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.Infrastructure.Services;

public class InMemoryCacheService : ICacheService
{
    private readonly ConcurrentDictionary<string, object> _cache = new();
    public Task<T?> GetAsync<T>(string key)
    {
        _cache.TryGetValue(key, out var val);
        return Task.FromResult((T?)val);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        _cache[key] = value;
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        _cache.TryRemove(key, out _);
        return Task.CompletedTask;
    }
}
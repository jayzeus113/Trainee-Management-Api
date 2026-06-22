using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;

namespace TraineeManagement.Services;

public class RedisCacheSercvice
{
    private readonly IDistributedCache _cache;

    public RedisCacheSercvice(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetKeyAsync<T>(string key, T t) {
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(t), cacheOptions);
    }

    public async Task <T?> GetKeyAsync<T>(string key)
    {
        string? cacheResponse = await _cache.GetStringAsync(key);
        return cacheResponse == null ? default : JsonSerializer.Deserialize<T>(cacheResponse);
    }
    

    public async Task DeleteKeyAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }

    public async Task<bool> ExistsKeyAsync(string key)
    {
        return _cache.GetAsync(key) != null;
    }
}
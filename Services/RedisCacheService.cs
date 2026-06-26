using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace TraineeManagement.Services;

public class RedisCacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisCacheService> _logger;


    public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task SetKeyAsync<T>(string key, T data) {
        _logger.LogDebug("Setting cache key: {CacheKey}", key);
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };
        try
        {
            var serializedData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(key, serializedData, cacheOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write data to Redis cache for key: {CacheKey}", key);
            throw;
        }
    }

    public async Task <T?> GetKeyAsync<T>(string key)
    {
        try
        {
            string? cacheResponse = await _cache.GetStringAsync(key);
            if (cacheResponse == null)
            {
                _logger.LogDebug("Cache miss for key: {CacheKey}", key);
                return default;
            }
            _logger.LogDebug("Cache hit for key: {CacheKey}", key);
            return JsonSerializer.Deserialize<T>(cacheResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to read data from Redis cache for key: {CacheKey}", key);
            throw;
        }
    }
    

    public async Task DeleteKeyAsync(string key)
    {
        _logger.LogDebug("Removing cache key: {CacheKey}", key);
        try
        {
            await _cache.RemoveAsync(key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete key from Redis cache: {CacheKey}", key);
            throw;
        }
    }

    public async Task<bool> ExistsKeyAsync(string key)
    {
        try
        {
            var data = await _cache.GetAsync(key);
            bool exists = data != null;
            _logger.LogDebug("Cache existence check for key: {CacheKey}. Exists: {Exists}", key, exists);
            return exists;
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to verify existence of Redis cache key: {CacheKey}", key);
            throw;
        }
    }
}
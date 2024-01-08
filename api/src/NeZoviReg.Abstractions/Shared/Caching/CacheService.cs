using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
#pragma warning disable CS8600

namespace NeZoviReg.Abstractions.Shared.Caching;

public interface ICacheService
{
    Task<T> GetAsync<T, TId>(string keyPrefix, TId keyDiscriminator, Func<Task<T>> factory, CancellationToken cancellationToken = default)
        where T : class?, new();

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    Task RemoveAsync<TId>(string keyPrefix, TId keyDiscriminator, CancellationToken cancellationToken = default);

    Task RemoveByPrefixAsync(string keyPrefix, CancellationToken cancellationToken = default);
}


public sealed class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private static readonly ConcurrentDictionary<string, string> CacheKeys = new();

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetAsync<T, TId>(string keyPrefix, TId keyDiscriminator, Func<Task<T>> factory, CancellationToken cancellationToken = default) 
        where T : class?,  new()
    {
        CacheKeys.TryAdd(keyPrefix, $"{keyPrefix}{keyDiscriminator}");

        return await _cache.GetOrCreateAsync($"{keyPrefix}{keyDiscriminator}", async ce =>
            {
                ConfigureCacheEntry(ce, GetTokenSource($"{keyPrefix}TokenSource"));

                return await factory().ConfigureAwait(false);
            })
            .ConfigureAwait(false) ?? new T();
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _cache.Remove(key);

        return Task.CompletedTask;
    }

    public Task RemoveAsync<TId>(string keyPrefix, TId keyDiscriminator, CancellationToken cancellationToken = default)
    {
        _cache.Remove($"{keyPrefix}{keyDiscriminator}");

        return Task.CompletedTask;
    }

    public Task RemoveByPrefixAsync(string keyPrefix, CancellationToken cancellationToken = default)
    {
        if (CacheKeys.TryGetValue(keyPrefix, out string key))
        {
            _cache.Remove(key);
        }

        return Task.CompletedTask;
    }

    private CancellationTokenSource GetTokenSource(string keyTokenSource) => _cache.GetOrCreate(keyTokenSource, _ => new CancellationTokenSource())!;

    private static void ConfigureCacheEntry(ICacheEntry cacheEntry, CancellationTokenSource cts)
    {
        cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
        cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
        cacheEntry.AddExpirationToken(new CancellationChangeToken(cts.Token));
    }
}
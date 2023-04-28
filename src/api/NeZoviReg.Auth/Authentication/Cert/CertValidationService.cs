using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;

namespace NeZoviReg.Auth.Authentication.Cert;

public class CertValidationService : ICertValidationService
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IMemoryCache _cache;
    private const string KeyTokenSource = "Cert_TokenSource";

    public CertValidationService(IRegUserDataStore regUserDataStore, IMemoryCache cache)
    {
        _regUserDataStore = regUserDataStore;
        _cache = cache;
    }

    public async Task<Guid?> ValidateCertificate(X509Certificate2 clientCertificate)
    {
        return await _cache.GetOrCreateAsync($"reg_user_{clientCertificate.Thumbprint}", async ce =>
        {
            ConfigureCacheEntry(ce, GetTokenSource());

            var regUser = await _regUserDataStore.GetByThumbprint(clientCertificate.Thumbprint)
                .ConfigureAwait(false);

            return regUser?.GuidId;
            })
            .ConfigureAwait(false) ?? null;
    }

    private CancellationTokenSource GetTokenSource() => _cache.GetOrCreate(KeyTokenSource, _ => new CancellationTokenSource())!;

    private static void ConfigureCacheEntry(ICacheEntry cacheEntry, CancellationTokenSource cts)
    {
        cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
        cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
        cacheEntry.AddExpirationToken(new CancellationChangeToken(cts.Token));
    }

}
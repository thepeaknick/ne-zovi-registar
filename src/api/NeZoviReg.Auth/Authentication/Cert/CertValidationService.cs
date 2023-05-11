using System.Security.Cryptography.X509Certificates;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Shared.Caching;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Auth.Authentication.Cert;

public class CertValidationService : ICertValidationService
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly ICacheService _cache;

    public CertValidationService(IRegUserDataStore regUserDataStore, ICacheService cache)
    {
        _regUserDataStore = regUserDataStore;
        _cache = cache;
    }

    public async Task<RegUser?> ValidateCertificate(X509Certificate2 clientCertificate, CancellationToken cancellationToken = default)
    {
        /*return await _cache.GetAsync(CacheKeyPrefix.Cert, clientCertificate.Thumbprint,
            async () =>
        {
            var regUser = await _regUserDataStore.GetByThumbprint(clientCertificate.Thumbprint, cancellationToken)
                .ConfigureAwait(false);

            return regUser;
        }, cancellationToken);*/

        return default;
    }
}
using System.Security.Cryptography.X509Certificates;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;

namespace NeZoviReg.Auth.Authentication.Cert;

public class CertValidationService : ICertValidationService
{
    private readonly IRegUserDataStore _regUserDataStore;

    public CertValidationService(IRegUserDataStore regUserDataStore)
    {
        _regUserDataStore = regUserDataStore;
    }

    /*public bool ValidateCertificate(X509Certificate2 clientCertificate)
    {
        string[] allowedThumbprints = { //TODO read from appsettings.json, or db
            "FC2A6F7D627E08FDAB50F194FEC535C7E21824C3"
            /*"D9B889793C876CF81307F9D2BA6F53C1D87E7EEE"#1#
        };
        return allowedThumbprints.Contains(clientCertificate.Thumbprint);
    }*/

    public async Task<Guid?> ValidateCertificate(X509Certificate2 clientCertificate)
    {
        var regUser = await _regUserDataStore.GetByThumbprint(clientCertificate.Thumbprint);

        return regUser?.GuidId;
    }
}
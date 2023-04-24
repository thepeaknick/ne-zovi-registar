using System.Security.Cryptography.X509Certificates;

namespace NeZoviReg.Auth.Authentication.Cert;

public class CertValidationService : ICertValidationService
{
    public bool ValidateCertificate(X509Certificate2 clientCertificate)
    {
        string[] allowedThumbprints = { //TODO read from appsettings.json, or db
            "FC2A6F7D627E08FDAB50F194FEC535C7E21824C3"
            /*"D9B889793C876CF81307F9D2BA6F53C1D87E7EEE"*/
        };
        return allowedThumbprints.Contains(clientCertificate.Thumbprint);
    }

    public int? ValidateCertificateWithUserId(X509Certificate2 clientCertificate)
    {
        string[] allowedThumbprints = { //TODO read from appsettings.json, or db
            "FC2A6F7D627E08FDAB50F194FEC535C7E21824C3"
            /*"D9B889793C876CF81307F9D2BA6F53C1D87E7EEE"*/
        };
        //1. get the user by mail in the cert
        //2 check the Thumbprint
        //3 return userId
        var valid = allowedThumbprints.Contains(clientCertificate.Thumbprint);

        if (valid)
        {
            return 1;
        }
        return default;
    }
}
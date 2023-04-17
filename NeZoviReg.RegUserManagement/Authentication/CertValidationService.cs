using System.Security.Cryptography.X509Certificates;

namespace NeZoviReg.Auth.Authentication;

public class CertValidationService : ICertValidationService
{
    public bool ValidateCertificate(X509Certificate2 clientCertificate) {
        string[] allowedThumbprints = { //TODO read from appsettings.json, or db
            "FC2A6F7D627E08FDAB50F194FEC535C7E21824C3"
        };

        return allowedThumbprints.Contains(clientCertificate.Thumbprint);
    }
}
using System.Security.Cryptography.X509Certificates;

namespace NeZoviReg.Auth.Authentication.Cert;

public interface ICertValidationService
{
    bool ValidateCertificate(X509Certificate2 clientCertificate);

    int? ValidateCertificateWithUserId(X509Certificate2 clientCertificate);
}
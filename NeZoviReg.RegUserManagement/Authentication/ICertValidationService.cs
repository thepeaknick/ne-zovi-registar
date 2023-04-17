using System.Security.Cryptography.X509Certificates;

namespace NeZoviReg.Auth.Authentication;

public interface ICertValidationService
{
    bool ValidateCertificate(X509Certificate2 clientCertificate);
}
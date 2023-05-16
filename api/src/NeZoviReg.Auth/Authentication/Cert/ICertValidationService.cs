using System.Security.Cryptography.X509Certificates;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Auth.Authentication.Cert;

public interface ICertValidationService
{
    Task<RegUser?> ValidateCertificate(X509Certificate2 clientCertificate, CancellationToken cancellationToken = default);
}
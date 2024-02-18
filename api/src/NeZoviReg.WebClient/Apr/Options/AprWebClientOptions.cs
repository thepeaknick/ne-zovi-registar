using System.Security.Cryptography.X509Certificates;
using NeZoviReg.WebClient.Options;

namespace NeZoviReg.WebClient.Apr.Options;

public class AprWebClientOptions : WebClientOptions
{
    public const string SectionName = "WebClient:Apr";
    
    public StoreLocation CertStore { get; set; } = StoreLocation.LocalMachine;

    public string? CertificateSerialNumber { get; set; }

    public CredentialsOptions Credentials { get; set; } = new CredentialsOptions();
}

public class CredentialsOptions
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
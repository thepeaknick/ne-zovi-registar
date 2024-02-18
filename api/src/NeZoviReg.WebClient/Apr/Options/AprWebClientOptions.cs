using System.Security.Cryptography.X509Certificates;
using NeZoviReg.WebClient.Options;

namespace NeZoviReg.WebClient.Apr.Options;

public class AprWebClientOptions : WebClientOptions
{
    public const string SectionName = "WebClient:Apr";
    
    public StoreLocation CertStore { get; set; }

    public string? CertificateSerialNumber { get; set; }

    public CredentialsOptions Credentials { get; set; } = new CredentialsOptions();
}

public class CredentialsOptions
{
    public const string SectionName = "WebClient:Apr:Credentials";
    public string? Username { get; set; }

    public string? Password { get; set; }
}
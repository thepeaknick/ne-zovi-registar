using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;

namespace NeZoviReg.WebClient.Apr.Options;

public class AprWebClientOptions
{
    public const string SectionName = "AprWebClient";
    
    public string? BaseUrl { get; set; }

    public StoreLocation CertStore { get; set; } = StoreLocation.LocalMachine;

    public string? CertificateSerialNumber { get; set; }

}
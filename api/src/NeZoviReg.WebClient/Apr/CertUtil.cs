using System.Security.Cryptography.X509Certificates;

namespace NeZoviReg.WebClient.Apr;

public static class CertUtil
{
    public static X509Certificate2?  GetX5092BySerialNumber(StoreLocation storeLocation, string serialNumber)
    {
        using var store = new X509Store(StoreName.My, storeLocation);
        store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

        return store.Certificates.FirstOrDefault(cert =>
            cert.SerialNumber is { } sn && sn.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
    }
}
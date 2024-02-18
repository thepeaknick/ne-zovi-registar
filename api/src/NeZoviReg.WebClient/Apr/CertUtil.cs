using System.Security.Cryptography.X509Certificates;

namespace NeZoviReg.WebClient.Apr;

public class CertUtil
{
    public static X509Certificate2?  GetX5092BySerialNumber(StoreLocation storeLocation, string serialNumber)
    {
        using var store = new X509Store(StoreName.My, storeLocation);
        store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

        foreach (X509Certificate2 cert in store.Certificates)
        {
            if (cert.SerialNumber is { } sn && sn.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                return cert;
        }

        return null;
    }
}
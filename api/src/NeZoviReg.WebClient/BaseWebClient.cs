using System.ServiceModel;
using Microsoft.Extensions.Options;
using NeZoviReg.WebClient.Apr.Options;

namespace NeZoviReg.WebClient;

public abstract class BaseWebClient
{
    protected readonly BasicHttpBinding HttpBinding;
    protected readonly EndpointAddress Endpoint;

    protected BaseWebClient(IOptionsSnapshot<AprWebClientOptions> options)
    {
        HttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
        HttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        HttpBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
        Endpoint = new EndpointAddress(new Uri(options.Value.BaseUrl!));
    }
}
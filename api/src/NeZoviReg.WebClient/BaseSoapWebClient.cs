using System.ServiceModel;
using Microsoft.Extensions.Options;
using NeZoviReg.WebClient.Options;

namespace NeZoviReg.WebClient;

/// <summary>
/// Base SOAP Web client abstration designed for inheritance.
/// Uses BasicHttpBinding as the only option
/// </summary>
public abstract class BaseSoapWebClient
{
    protected readonly BasicHttpBinding HttpBinding = new BasicHttpBinding();
    protected EndpointAddress Endpoint = default!;

    protected BaseSoapWebClient(IOptionsSnapshot<WebClientOptions> options)
    {
        Init(options.Value);
    }


    private void Init(WebClientOptions options)
    {
        SetBinding();
        Endpoint = new EndpointAddress(new Uri(options.BaseUrl!));
    }

    protected virtual void SetBinding()
    {
        SetBindingSecurityMode();
        SetBindingTransportClientCredentialType();
        SetBindingMessageClientCredentialType();
    }

    protected virtual void SetBindingSecurityMode()
    {
        HttpBinding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
    }

    protected virtual void SetBindingTransportClientCredentialType()
    {
        HttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
    }

    protected virtual void SetBindingMessageClientCredentialType()
    {
        HttpBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
    }
}
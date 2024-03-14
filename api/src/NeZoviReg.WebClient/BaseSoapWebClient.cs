using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Extensions.Options;
using NeZoviReg.WebClient.Options;

namespace NeZoviReg.WebClient;

/// <summary>
/// Base SOAP Web client abstration designed for inheritance.
/// Uses CustomBinding as the only option.
/// </summary>
public abstract class BaseSoapWebClient
{
    protected readonly CustomBinding CustomBinding = new CustomBinding();
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
        SetBindingMessageEncoding();
        SetBindingTransport();
    }

    protected virtual void SetBindingSecurityMode()
    {
        var security = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
        security.IncludeTimestamp = false;
        CustomBinding.Elements.Add(security);
    }

    protected virtual void SetBindingMessageEncoding()
    {
        CustomBinding.Elements.Add(new TextMessageEncodingBindingElement
        {
            MessageVersion = MessageVersion.Soap11,
            WriteEncoding = System.Text.Encoding.UTF8
        });
    }

    protected virtual void SetBindingTransport()
    {
        CustomBinding.Elements.Add(new HttpsTransportBindingElement());
    }
}
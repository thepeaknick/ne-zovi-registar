using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using NeZoviReg.Abstractions.Shared.Model.AprBusinessEntity;
using NeZoviReg.WebClient.Apr.Options;
using NeZoviReg.WebClient.PlService;

namespace NeZoviReg.WebClient.Apr;

public class AprSoapWebClient : IAprWebClient
{
    private readonly ILogger<AprSoapWebClient> _logger;
    private readonly IMapper _mapper;
    private readonly BasicHttpBinding _httpBinding;
    private readonly EndpointAddress _endpoint;
    private readonly AprWebClientOptions _options;
    
    public AprSoapWebClient(IOptions<AprWebClientOptions> options, IMapper mapper, ILogger<AprSoapWebClient> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _options = options.Value;
        _httpBinding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
        _httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        _httpBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
        _endpoint = new EndpointAddress(new Uri(_options.BaseUrl!));
    }

    public async Task<List<AprBusinessEntity>> GetAprData(string regNumber, CancellationToken cancellationToken)
    {
        await using var client = PlServiceClient();
        var data = await client.PreuzmiPodatkeOPrivrednomSubjektuAsync(new PrivredniSubjektiUlazniPodaci()
        {
            privredniSubjekti = new PrivredniSubjekatMaticniBroj()
            {
                maticniBroj = regNumber
            }
        });

        return _mapper.Map<List<AprBusinessEntity>>(data);
    }

    private PlServiceClient PlServiceClient()
    {
        var client = new PlServiceClient(_httpBinding, _endpoint);
        client.ChannelFactory.Credentials.ClientCertificate.Certificate = GetCert();
        client.ChannelFactory.Credentials.UserName.UserName = "??";
        client.ChannelFactory.Credentials.UserName.Password = "??";

        return client;
    }
    private X509Certificate2? GetCert()
    {
        var serialNumber = _options.CertificateSerialNumber;
        
        _logger.LogInformation($"Using client certificate {serialNumber}");

        var cert =  CertUtil.GetX5092BySerialNumber(_options.CertStore, serialNumber!);

        if (cert is null)
        {
            _logger.LogError($"Certificate with {serialNumber} not found.");
        }

        return cert;
    }
}
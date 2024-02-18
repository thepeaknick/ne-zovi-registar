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

public class AprSoapWebClient : BaseWebClient, IAprWebClient
{
    private readonly ILogger<AprSoapWebClient> _logger;
    private readonly IMapper _mapper;
    private readonly AprWebClientOptions _options;

    public AprSoapWebClient(IOptions<AprWebClientOptions> options, IMapper mapper, ILogger<AprSoapWebClient> logger)
        : base(options)
    {
        _logger = logger;
        _mapper = mapper;
        _options = options.Value;
    }

    public async Task<List<AprBusinessEntity>> GetAprData(string regNumber, CancellationToken cancellationToken)
    {
        await using var client = PlServiceClient();
        
        var data = await client.PreuzmiPodatkeOPrivrednomSubjektuAsync(new PrivredniSubjektiUlazniPodaci
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
        var client = new PlServiceClient(HttpBinding, Endpoint);
        client.ChannelFactory.Credentials.ClientCertificate.Certificate = GetCert();
        client.ChannelFactory.Credentials.UserName.UserName = "??";
        client.ChannelFactory.Credentials.UserName.Password = "??";

        return client;
    }
    private X509Certificate2? GetCert()
    {
        _logger.LogInformation($"Using client certificate SN={_options.CertificateSerialNumber}");

        var cert =  CertUtil.GetX5092BySerialNumber(_options.CertStore, _options.CertificateSerialNumber!);

        if (cert is null)
        {
            _logger.LogError($"Certificate with SN={_options.CertificateSerialNumber} not found.");
        }

        return cert;
    }
}
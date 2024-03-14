using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using NeZoviReg.Abstractions.Shared.Model.AprBusinessEntity;
using NeZoviReg.WebClient.Apr.Options;
using NeZoviReg.WebClient.PlService;

namespace NeZoviReg.WebClient.Apr;

/// <summary>
/// APR service SOAP client decorator.
/// </summary>
public class AprSoapWebClient : BaseSoapWebClient, IAprWebClient
{
    private readonly ILogger<AprSoapWebClient> _logger;
    private readonly IMapper _mapper;
    private readonly AprWebClientOptions _options;

    public AprSoapWebClient(IOptionsSnapshot<AprWebClientOptions> options, IMapper mapper, ILogger<AprSoapWebClient> logger)
        : base(options)
    {
        _logger = logger;
        _mapper = mapper;
        _options = options.Value;
    }

    public async Task<List<AprBusinessEntity>> GetAprBusinessEntitiesAsync(string regNumber, CancellationToken cancellationToken = default)
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

    public async Task<AprBusinessEntity> GetAprBusinessEntityAsync(string regNumber, CancellationToken cancellationToken = default)
    {
        await using var client = PlServiceClient();
        
        var data = await client.PreuzmiPodatkeOPrivrednomSubjektuAsync(new PrivredniSubjektiUlazniPodaci
        {
            privredniSubjekti = new PrivredniSubjekatMaticniBroj()
            {
                maticniBroj = regNumber
            }
        });

        return _mapper.Map<AprBusinessEntity>(data.FirstOrDefault());
    }
    private PlServiceClient PlServiceClient()
    {
        var client = new PlServiceClient(CustomBinding, Endpoint);
        SetCredentials(client);
        return client;
    }

    private void SetCredentials(PlServiceClient client)
    {
        _logger.LogInformation($"Setting up {nameof(PlServiceClient)} Credentials={_options.Credentials.Username}/{_options.Credentials.Password}");
        
        client.ClientCredentials.UserName.UserName = _options.Credentials.Username;
        client.ClientCredentials.UserName.Password = _options.Credentials.Password;
    }
}
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using NeZoviReg.Abstractions.Shared.Model.Infrastructure;
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
    private readonly PlServiceClient _client;

    public AprSoapWebClient(IOptionsSnapshot<AprWebClientOptions> options, IMapper mapper,
        ILogger<AprSoapWebClient> logger)
        : base(options)
    {
        _logger = logger;
        _mapper = mapper;
        _options = options.Value;
        _client = PlServiceClient();
    }
   

    public async Task<AprBusinessEntity?> GetAprBusinessEntityAsync(string regNumber,
        CancellationToken cancellationToken = default)
    {
        var aprData = await GetAprData(regNumber, PrivredniSubjekatMaticniBrojTip.Item1); //DOO

        aprData ??= await GetAprData(regNumber, PrivredniSubjekatMaticniBrojTip.Item2); //Preduzetnici

        aprData ??= await GetAprData(regNumber, PrivredniSubjekatMaticniBrojTip.Item3); //Udruzenje
        
        aprData ??= await GetAprData(regNumber, PrivredniSubjekatMaticniBrojTip.Item4); //Stec. masa
        
        aprData ??= await GetAprData(regNumber, PrivredniSubjekatMaticniBrojTip.Item5); //Fondacija
        
        aprData ??= await GetAprData(regNumber, PrivredniSubjekatMaticniBrojTip.Item6); //Sport. udruz.
        
        aprData ??= await GetAprData(regNumber, PrivredniSubjekatMaticniBrojTip.Item7); //Komora
        
        return _mapper.Map<AprBusinessEntity?>(aprData);
    }

    private async Task<(PrivredniSubjekat? AprData, PrivredniSubjekatMaticniBrojTip Tip)?> GetAprData(string regNumber, PrivredniSubjekatMaticniBrojTip tip)
    {
        PrivredniSubjekat[] data;
        try
        {
            data = await _client.PreuzmiPodatkeOPrivrednomSubjektuAsync(new PrivredniSubjektiUlazniPodaci
            {
                privredniSubjekti = new PrivredniSubjekatMaticniBroj
                {
                    tip = tip,
                    maticniBroj = regNumber
                }
            });
        }
        catch (Exception e)
        {
            _logger.LogInformation($"MaticniBroj={regNumber};Tip={tip} ne postoji u APR-u");
            return default;
        }

        var aprData = data?.FirstOrDefault();
        
        switch (tip)
        {
            //aktivan
            case PrivredniSubjekatMaticniBrojTip.Item1 //DOO
                when aprData?.grupa.FirstOrDefault(y => y.id == "1002")?.podatak.FirstOrDefault(p => p.naziv == "IdentifikatorStatusa")?.vrednost == "2":
                return (aprData, tip);
            //aktivan
            case PrivredniSubjekatMaticniBrojTip.Item2 //Preduzetnik
                when aprData?.grupa.FirstOrDefault(y => y.id == "1042")?.podatak.FirstOrDefault(p => p.naziv == "IdentifikatorStatusa")?.vrednost == "3":
                return (aprData, tip);
            //aktivan
            case PrivredniSubjekatMaticniBrojTip.Item3 //Udruzenje
                when aprData?.grupa.FirstOrDefault(y => y.id == "57")?.podatak.FirstOrDefault(p => p.naziv == "IdentifikatorStatusa")?.vrednost == "2":
                return (aprData, tip);
            //aktivan
            case PrivredniSubjekatMaticniBrojTip.Item5 //Fondacija
                when aprData?.grupa.FirstOrDefault(y => y.id == "81")?.podatak.FirstOrDefault(p => p.naziv == "IdentifikatorStatusa")?.vrednost == "2":
                return (aprData, tip);
            //aktivan
            case PrivredniSubjekatMaticniBrojTip.Item6 //Sports. udruzenja
                when aprData?.grupa.FirstOrDefault(y => y.id == "11")?.podatak.FirstOrDefault(p => p.naziv == "IdentifikatorStatusa")?.vrednost == "2":
                return (aprData, tip);
            //aktivan uvek (nema status identifikator)
            case PrivredniSubjekatMaticniBrojTip.Item4: //Stec. masa
            case PrivredniSubjekatMaticniBrojTip.Item7: //Komora
                return (aprData, tip);
            default:
                return default;
        }
    }

    private PlServiceClient PlServiceClient()
    {
        var client = new PlServiceClient(CustomBinding, Endpoint);
        SetCredentials(client);
        return client;
    }

    private void SetCredentials(PlServiceClient client)
    {
        _logger.LogInformation(
            $"Setting up {nameof(PlServiceClient)} Credentials={_options.Credentials.Username}/{_options.Credentials.Password}");

        client.ClientCredentials.UserName.UserName = _options.Credentials.Username;
        client.ClientCredentials.UserName.Password = _options.Credentials.Password;
    }
}
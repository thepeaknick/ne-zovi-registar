using NeZoviReg.Abstractions.Shared.Model.AprBusinessEntity;

namespace NeZoviReg.Abstractions.Infrastructure.WebClient;

public interface IAprWebClient
{
    Task<List<AprBusinessEntity>> GetAprData(string regNumber, CancellationToken cancellationToken);

}
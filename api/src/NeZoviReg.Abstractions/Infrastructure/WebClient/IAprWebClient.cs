using NeZoviReg.Abstractions.Shared.Model.AprBusinessEntity;

namespace NeZoviReg.Abstractions.Infrastructure.WebClient;

public interface IAprWebClient
{
    Task<List<AprBusinessEntity>> GetAprBusinessEntitiesAsync(string regNumber, CancellationToken cancellationToken = default);

    Task<AprBusinessEntity> GetAprBusinessEntityAsync(string regNumber, CancellationToken cancellationToken = default);
}
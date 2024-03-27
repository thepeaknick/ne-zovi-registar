using NeZoviReg.Abstractions.Shared.Model.Infrastructure;

namespace NeZoviReg.Abstractions.Infrastructure.WebClient;

public interface IAprWebClient
{
    Task<AprBusinessEntity?> GetAprBusinessEntityAsync(string regNumber, CancellationToken cancellationToken = default);
}
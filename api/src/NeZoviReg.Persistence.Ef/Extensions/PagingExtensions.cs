using Microsoft.EntityFrameworkCore;
using NeZoviReg.Domain;

namespace NeZoviReg.Abstractions.Extensions.Paging;

public static class RepositoryExtensions
{
    public static async Task<PagedList<T>> GetPagedAsync<T>(this IQueryable<T> query,
        PageInfo pageInfo,
        CancellationToken cancellationToken = default) where T : IEntity
    {
        var count = await query.CountAsync(cancellationToken);

        return await query.GetPagedNoCountAsync(pageInfo, count, cancellationToken);
    }

    public static Task<PagedList<T>> GetPagedAsync<T>(this IQueryable<T> query,
        long cursor,
        int pageSize,
        CancellationToken cancellationToken = default) where T : IEntity
    {
        return query.GetPagedAsync(new PageInfo { CurrentCursor = cursor, PageSize = pageSize }, cancellationToken);
    }

    public static async Task<PagedList<T>> GetPagedNoCountAsync<T>(this IQueryable<T> query,
        PageInfo pageInfo,
        int totalCount = 0,
        CancellationToken cancellationToken = default) where T : IEntity
    {
        List<T> data = await query
            .Where(p => p.Id > pageInfo.CurrentCursor)
            .Take(pageInfo.PageSize)
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);

        pageInfo.TotalCount = totalCount == 0 ? data.Count : totalCount;
        pageInfo.CurrentCursor = data[^1].Id;
        
        return new PagedList<T>(data, pageInfo);
    }
}
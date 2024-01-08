namespace NeZoviReg.Abstractions.Extensions.Paging;

/// <summary>
/// Kolekcija sa opcijom straničenja.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class PagedList<TItem>
{
    public PagedList()
        : this(null, new PageInfo())
    {
    }

    public PagedList(PageInfo pageInfo)
        : this(null, pageInfo)
    {
    }

    public PagedList(IEnumerable<TItem> items, PageInfo pageInfo)
    {
        PageInfo = pageInfo;
        Items = items as List<TItem> ?? new List<TItem>(items ?? Enumerable.Empty<TItem>());
    }

    /// <summary>
    /// Kolekcija
    /// </summary>
    public List<TItem> Items { get; }

    /// <summary>
    /// Opis svake strane.
    /// </summary>
    public PageInfo PageInfo { get; }
    
}

using System.Diagnostics;

namespace NeZoviReg.Abstractions.Extensions.Paging;

/// <summary>
/// Opis svake strane.
/// </summary>
[DebuggerDisplay(DebuggerDisplayStr)]
public class PageInfo
{
    private const string DebuggerDisplayStr = "cursor {" + nameof(CurrentCursor) + "}, " +
                                              "size {" + nameof(PageSize) + "}, " +
                                              "count {" + nameof(TotalCount) + "}";

    /// <summary>
    /// Id poslednjeg elementa na traženoj strani (Kursor).
    /// </summary>
    public long CurrentCursor { get; set; }

    /// <summary>
    /// Broj elemenata kolekcije u jednoj stranici.
    /// </summary>
    public int PageSize { get; set; } = 5;

    /// <summary>
    /// Ukupan broj elemenata u traženoj kolekciji
    /// </summary>
    public int TotalCount { get; set; }


    public override bool Equals(object obj)
    {
        return obj is PageInfo info &&
               CurrentCursor == info.CurrentCursor &&
               PageSize == info.PageSize &&
               TotalCount == info.TotalCount;
    }

    public override int GetHashCode()
    {
        var hashCode = -534682226;
        hashCode = (hashCode * -1521134295) + CurrentCursor.GetHashCode();
        hashCode = (hashCode * -1521134295) + PageSize.GetHashCode();
        hashCode = (hashCode * -1521134295) + TotalCount.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        var tc = TotalCount > 0 && TotalCount < int.MaxValue ? $",Total:{TotalCount}" : null;

        return $"Cursor:{CurrentCursor},PageSize:{PageSize}{tc}";
    }
}
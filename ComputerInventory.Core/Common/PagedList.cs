using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.Core.Common;

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public MetaData MetaData { get; }

    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            PageSize = pageSize,
            TotalCount = count
        };

        Items = new List<T>(items).AsReadOnly();
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        pageSize = Math.Min(pageSize, 100); // Max 100 per sida
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
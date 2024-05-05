using System.Data;

namespace ATech.BulkWriter.Extensions;

public static class IEnumerableExtensions
{
    public static DataTable ToDataTable<T>(this IEnumerable<T> source)
    {
        return new ObjectShredder<T>().Shred(source, null, null);
    }

    public static DataTable ToDataTable<T>(this IEnumerable<T> source,
                                                DataTable table, LoadOption? options)
    {
        return new ObjectShredder<T>().Shred(source, table, options);
    }
}

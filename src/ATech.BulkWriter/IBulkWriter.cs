namespace ATech.BulkWriter;

public interface IBulkWriter<in TEntity> : IDisposable
{
    void AddRange(IEnumerable<TEntity> entities, string? tableName = null);

    Task AddRangeAsync(IEnumerable<TEntity> entities, string? tableName = null, CancellationToken cancellationToken = default);
}

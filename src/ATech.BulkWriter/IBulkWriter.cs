namespace ATech.BulkWriter;

public interface IBulkWriter<in TEntity> : IDisposable
{
    void AddRange(IEnumerable<TEntity> entities);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}

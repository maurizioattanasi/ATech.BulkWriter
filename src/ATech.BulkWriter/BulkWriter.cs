using Microsoft.Data.SqlClient;

using ATech.BulkWriter.Extensions;

namespace ATech.BulkWriter;

public sealed class BulkWriter<TEntity> : IBulkWriter<TEntity>
{
    private readonly SqlBulkCopy _sqlBulkCopy;

    public BulkWriter(SqlConnection connection, string? tableName = null, string schema = "dbo")
    {
        _sqlBulkCopy = new SqlBulkCopy(connection);
        _sqlBulkCopy.DestinationTableName = $"{schema}.{tableName ?? typeof(TEntity).Name}";
    }

        public int BatchSize
    {
        get => _sqlBulkCopy.BatchSize;
        set => _sqlBulkCopy.BatchSize = value;
    }

    public int BulkCopyTimeout
    {
        get => _sqlBulkCopy.BulkCopyTimeout;
        set => _sqlBulkCopy.BulkCopyTimeout = value;
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        if (!entities.Any()) return;

        var table = entities.ToDataTable();

        _sqlBulkCopy.WriteToServer(table);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (!entities.Any()) return;

        var table = entities.ToDataTable();

        await _sqlBulkCopy.WriteToServerAsync(table, cancellationToken).ConfigureAwait(false);
    }

    public void Dispose() => ((IDisposable)_sqlBulkCopy).Dispose();
}
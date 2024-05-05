using System.Data.SqlClient;

using Microsoft.Extensions.Logging;

namespace ATech.BulkWriter;

public class BulkWriter<TEntity> : IBulkWriter<TEntity>
{
    private readonly SqlConnection _connection;
    private readonly ILogger<BulkWriter<TEntity>> _logger;

    private readonly string _schema;


    public BulkWriter(SqlConnection connection, ILogger<BulkWriter<TEntity>> logger, string schema = "dbo")
    {
        _connection = connection;
        _logger = logger;

        _connection.InfoMessage += (s, e) =>
        {
            foreach (SqlError error in e.Errors)
                _logger.LogError(error.Message);
        };
        _schema = schema;

    }

    public void AddRange(IEnumerable<TEntity> entities, string? tableName = null)
    {
       try
        {
            if (!entities.Any()) return;

            var table = entities.ToDataTable();

            _connection.Open();

            using var bulkCopy = new SqlBulkCopy(_connection);

            bulkCopy.BatchSize = table.Rows.Count;
            bulkCopy.BulkCopyTimeout = 0;

            bulkCopy.DestinationTableName = $"{_schema}.{tableName ?? typeof(TEntity).Name}";

            bulkCopy.WriteToServer(table);

            bulkCopy.Close();
        }
        catch (Exception ex)
        {
            var content = ex.InnerException is null ? ex.Message : ex.InnerException.Message;
            _logger.LogError(ex, content);
        }
        finally
        {
            _connection.Close();
        }
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities, string? tableName = null, CancellationToken cancellationToken = default)
    {
         try
        {
            if (!entities.Any()) return;

            var table = entities.ToDataTable();

            await _connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            using var bulkCopy = new SqlBulkCopy(_connection);

            bulkCopy.BatchSize = table.Rows.Count;
            bulkCopy.BulkCopyTimeout = 0;

            bulkCopy.DestinationTableName = $"{_schema}.{tableName ?? typeof(TEntity).Name}";

            await bulkCopy.WriteToServerAsync(table, cancellationToken).ConfigureAwait(false);

            bulkCopy.Close();
        }
        catch (Exception ex)
        {
            var content = ex.InnerException is null ? ex.Message : ex.InnerException.Message;
            _logger.LogError(ex, content);
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public void Dispose() => _connection.Dispose();
}
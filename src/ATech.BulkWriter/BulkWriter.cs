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
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities, string? tableName = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose() => _connection.Dispose();
}
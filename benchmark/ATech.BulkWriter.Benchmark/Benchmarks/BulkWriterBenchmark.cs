using BenchmarkDotNet.Attributes;

namespace ATech.BulkWriter.Benchmark;

public class BulkWriterBenchmark : BenchmarkBaseClass
{
    [Benchmark]
    public async Task BulkWriter()
    {
        await using var sqlConnection = DbHelpers.OpenSqlConnection();
        using var bulkWriter = new BulkWriter<DomainEntity>(sqlConnection)
        {
            BulkCopyTimeout = 0,
            BatchSize = 10000
        };

        var items = GetTestRecords();
        await bulkWriter.AddRangeAsync(items);
    }
}

using BenchmarkDotNet.Attributes;

namespace ATech.BulkWriter.Benchmark;

public class BenchmarkBaseClass
{
    [GlobalSetup]
    public virtual void GlobalSetup()
    {
        using var sqlConnection = DbHelpers.OpenSqlConnection();
        DbHelpers.TruncateTable<DomainEntity>(sqlConnection);
    }

    protected IEnumerable<DomainEntity> GetTestRecords()
    {
        return DataGenerationHelpers.GetDomainEntities(1000);
    }
}

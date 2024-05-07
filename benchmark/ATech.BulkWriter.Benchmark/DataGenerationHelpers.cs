namespace ATech.BulkWriter.Benchmark;

internal static class DataGenerationHelpers
{
    private static long _idCounter = 0;

    public static IEnumerable<DomainEntity> GetDomainEntities(int count)
    {
        for (var i = 0; i < count; i++)
        {
            yield return new DomainEntity
            {
                Id = GetNextId(),
                FirstName = $"Bob-{i}",
                LastName = $"Smith-{i}"
            };
        }
    }

    private static long GetNextId()
    {
        return Interlocked.Increment(ref _idCounter);
    }
}

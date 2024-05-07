using System.ComponentModel.DataAnnotations;

namespace ATech.BulkWriter.Benchmark;

public class DomainEntity
{
    [Key]
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}

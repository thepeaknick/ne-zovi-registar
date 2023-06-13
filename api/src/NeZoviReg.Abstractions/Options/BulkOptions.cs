using System.ComponentModel.DataAnnotations;

namespace NeZoviReg.Abstractions.Options;

public class BulkOptions
{
    public const string SectionName = "Bulk";
    
    [Required]
    public int BatchSize { get; set; }
}
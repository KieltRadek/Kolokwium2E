using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2E.Models;

[PrimaryKey(nameof(BatchId), nameof(EmployeeId))]
public class Responsible
{
    [ForeignKey(nameof(BatchId))]
    public int BatchId { get; set; }
    
    [ForeignKey(nameof(EmployeeId))]
    public int EmployeeId { get; set; }
    
    public SeedingBatch Batch { get; set; }
    public Employee Employee { get; set; }
    
    [MaxLength(100)]
    public string Role { get; set; }
}
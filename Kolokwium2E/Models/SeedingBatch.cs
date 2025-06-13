using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2E.Models;

public class SeedingBatch
{
    [Key]
    public int BatchId { get; set; }
    [ForeignKey(nameof(NurseryId))]
    public int NurseryId { get; set; }
    
    [ForeignKey(nameof(SpeciesId))]
    public int SpeciesId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }
    
    public Nursery Nursery { get; set; }
    public TreeSpecies Species { get; set; }
    
    public ICollection<Responsible> Responsibles { get; set; }
    public ICollection<TreeSpecies> TreeSpecies { get; set; }
}
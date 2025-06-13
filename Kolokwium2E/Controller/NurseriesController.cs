using Kolokwium2E.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2E.Controller;

[ApiController]
[Route("api/nurseries")]
public class NurseriesController : ControllerBase
{
    private readonly NurseryContext _context;

    public NurseriesController(NurseryContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/batches")]
    public async Task<IActionResult> GetNurseryBatches(int id)
    {
        var nursery = await _context.Nurseries
            .Include(n => n.SeedingBatches)
            .ThenInclude(b => b.TreeSpecies)
            .Include(n => n.SeedingBatches)
            .ThenInclude(b => b.Responsibles)
            .ThenInclude(br => br.Employee)
            .FirstOrDefaultAsync(n => n.NurseryId == id);

        if (nursery == null)
            return NotFound();

        var result = new
        {
            nurseryId = nursery.NurseryId,
            name = nursery.Name,
            establishedDate = nursery.EstablishedDate,
            batches = nursery.SeedingBatches.Select(b => new
            {
                batchId = b.BatchId,
                quantity = b.Quantity,
                sownDate = b.SownDate,
                readyDate = b.ReadyDate,
                species = new
                {
                    //latinName = b.TreeSpecies.LatinName,
                    //growthTimeInYears = b.TreeSpecies.GrowthTimeInYears
                },
                responsible = b.Responsibles.Select(r => new
                {
                    firstName = r.Employee.FirstName,
                    lastName = r.Employee.LastName,
                    role = r.Role
                })
            })
        };

        return Ok(result);
    }
}

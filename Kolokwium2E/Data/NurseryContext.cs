using System.Data;
using Microsoft.EntityFrameworkCore;
using Kolokwium2E.Models;

namespace Kolokwium2E.Data;

public class NurseryContext : DbContext
{
    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<TreeSpecies> TreeSpecies { get; set; }
    public DbSet<SeedingBatch> SeedingBatches { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }

    protected NurseryContext()
    {
    }

    public NurseryContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Responsible>()
            .HasKey(br => new { br.BatchId, br.EmployeeId });

        modelBuilder.Entity<Responsible>()
            .HasOne(br => br.Batch)
            .WithMany(b => b.Responsibles)
            .HasForeignKey(br => br.BatchId);

        modelBuilder.Entity<Responsible>()
            .HasOne(br => br.Employee)
            .WithMany(e => e.Responsibles)
            .HasForeignKey(br => br.EmployeeId);

        modelBuilder.Entity<Nursery>().HasData(new Nursery
            { NurseryId = 1, Name = "Green Forest Nursery", EstablishedDate = new DateTime(2005, 05, 10) });
        modelBuilder.Entity<TreeSpecies>().HasData(new TreeSpecies
            { SpeciesId = 1, LatinName = "Quercus robur", GrowthTimeInYears = 5 });
        modelBuilder.Entity<Employee>().HasData(
            new Employee
                { EmployeeId = 1, FirstName = "Anna", LastName = "Kowalska", HireDate = new DateTime(2010, 01, 15) },
            new Employee
                { EmployeeId = 3, FirstName = "Jan", LastName = "Nowak", HireDate = new DateTime(2012, 04, 20) }
        );
    }
}



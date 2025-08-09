using ManagementApi.Employees;
using ManagementApi.OrganizationUnits;
using ManagementApi.Positions;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Position> Positions { get; set; }

    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }


    public DbSet<OrganizationUnitEmployee> OrganizationUnitEmployees { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<OrganizationUnit>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.HasOne(e => e.Parent)
                  .WithMany(e => e.Children)
                  .HasForeignKey(e => e.ParentId)
                  .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        });


        modelBuilder.Entity<OrganizationUnitEmployee>(entity =>
        {
            entity.HasKey(x => new { x.EmployeeId, x.OrganizationUnitId });
        });

        base.OnModelCreating(modelBuilder);
    }
}

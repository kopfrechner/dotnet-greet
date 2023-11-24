using Objectbay.Vacation.WebApi.DbModels;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
       : base(options) { }

    public DbSet<VacationItem> VacationItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VacationItem>().HasData(
            new VacationItem
            {
                Id = Guid.Parse("3fb362d5-18c3-45de-813e-6147adbc3d9a"),
                Name = "Bikini",
                Category = VacationItemCategory.CLOTHING
            },
            new VacationItem
            {
                Id = Guid.Parse("c37f373d-6869-48a2-a8c0-2e3b2626da6a"),
                Name = "Sonnencreme",
                Category = VacationItemCategory.SURVIVAL_ESSENTIALS
            }
        );
    }
}
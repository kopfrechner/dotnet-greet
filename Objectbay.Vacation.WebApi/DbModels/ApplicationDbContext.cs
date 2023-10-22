using Objectbay.Vacation.WebApi.DbModels;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options) { }

    public DbSet<VacationItem> VacationItems { get; set; } = null!;
}
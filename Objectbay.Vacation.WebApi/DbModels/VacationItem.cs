using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Objectbay.Vacation.WebApi.DbModels;

public record VacationItem
{
    public Guid Id { get; init; }

    public required string Name { get; set; }
    public VacationItemCategory? Category { get; set; }

    public Guid? VacationId { get; set; }
    public virtual Vacation? Vacation { get; set; }
}

public record Vacation
{
    public Guid Id { get; init; }
    public required string Name { get; set; }

    public virtual Collection<VacationItem>? VacationItems { get; set; }
}

public class VacationConfiguration
    : IEntityTypeConfiguration<Vacation>
{
    public void Configure(EntityTypeBuilder<Vacation> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired();

        builder.HasMany(x => x.VacationItems)
            .WithOne(y => y.Vacation)
            .HasForeignKey(x => x.VacationId);
    }
}


public class VacationItemConfiguration
    : IEntityTypeConfiguration<VacationItem>
{
    public void Configure(EntityTypeBuilder<VacationItem> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired();
    }
}

public enum VacationItemCategory
{
    CLOTHING,
    ACCESSORIES,
    ELECTRONICS,
    SURVIVAL_ESSENTIALS
}
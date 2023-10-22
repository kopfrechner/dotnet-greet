using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_greet.DbModels;

public class VacationItem
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public VacationItemCategory? Category { get; set; }
}


public class VacationItemConfiguration : IEntityTypeConfiguration<VacationItem>
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
    ACCESSIOURS,
    ELECTRONICS,
    SURVIVAL_ESSENTIALS
}
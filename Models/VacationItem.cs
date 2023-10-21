using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VacationItem {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public VacationItemCategory? Category{ get; set; }
}


public class VacationItemConfiguration : IEntityTypeConfiguration<VacationItem>
{
    public void Configure(EntityTypeBuilder<VacationItem> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired();
    }
}

public enum VacationItemCategory {
    CLOTHING,
    ACCESSIOURS,
    ELECTRONICS
}
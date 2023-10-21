using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VacationItem {
    public Guid Id { get; }
    public string Name { get; set; }
    public VacationItemCategory Category{ get; set; }
}


public class VacationItemConfiguration : IEntityTypeConfiguration<VacationItem>
{
    public void Configure(EntityTypeBuilder<VacationItem> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.Category);
    }
}

public enum VacationItemCategory {
    CLOTHING,
    ACCESSIOURS,
    ELECTRONICS
}
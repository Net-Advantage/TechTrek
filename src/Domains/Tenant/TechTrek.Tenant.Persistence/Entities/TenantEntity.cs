namespace TechTrek.Tenant.Persistence.Entities;

public sealed partial class TenantEntity : ObservableObject
{
    [ObservableProperty]
    private Guid _id = Guid.NewGuid();

    [ObservableProperty]
    private string _name = default!;
}

internal sealed class TenantEntityConfiguration : IEntityTypeConfiguration<TenantEntity>
{
    public void Configure(EntityTypeBuilder<TenantEntity> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

    }
}
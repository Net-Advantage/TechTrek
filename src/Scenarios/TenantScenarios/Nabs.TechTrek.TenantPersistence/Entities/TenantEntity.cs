using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nabs.TechTrek.TenantPersistence.Entities;

public sealed class TenantEntity : EntityBase<Guid>
{
    public string Name { get; set; } = default!;
    public TenantIsolationStrategy IsolationStrategy { get; set; }
}

internal sealed class TenantEntityConfiguration : IEntityTypeConfiguration<TenantEntity>
{
    public TenantEntityConfiguration()
    {

    }

    public void Configure(EntityTypeBuilder<TenantEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
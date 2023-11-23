
namespace Nabs.TechTrek.Persistence.Entities;

public sealed class WeatherForecastCommentEntity
{
    public Guid Id { get; set; }
    public int WeatherForecastId { get; set; }
    public Guid TenantId { get; set; }
    public Guid UserId { get; set; }
    public string? Comment { get; set; }
}

internal sealed class WeatherForecastCommentEntityConfiguration : IEntityTypeConfiguration<WeatherForecastCommentEntity>
{
    public void Configure(EntityTypeBuilder<WeatherForecastCommentEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();
    }
}
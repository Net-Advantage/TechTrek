namespace Nabs.TechTrek.Persistence.Entities;

public sealed class WeatherForecastEntity
{
	public int Id { get; set; }
	public DateOnly Date { get; set; }
	public double TemperatureC { get; set; }
	public double TemperatureF { get; set; }
	public string? Summary { get; set; }
}

internal sealed class WeatherForecastEntityConfiguration : IEntityTypeConfiguration<WeatherForecastEntity>
{
	public void Configure(EntityTypeBuilder<WeatherForecastEntity> builder)
	{
		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.ValueGeneratedNever();
		
		builder
			.Property(x => x.Date)
			.IsRequired();

		builder
			.Property(x => x.TemperatureC)
			.IsRequired();

		builder
			.Property(x => x.TemperatureF)
			.IsRequired();

		builder
			.Property(x => x.Summary)
			.IsRequired(false);

		builder
			.HasIndex(x => x.Date);
	}
}
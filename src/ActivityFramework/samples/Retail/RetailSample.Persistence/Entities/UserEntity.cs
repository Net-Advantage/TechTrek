namespace Nabs.TechTrek.Persistence.Entities;

public sealed class UserEntity : EntityBase<Guid>
{
	public string Username { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
}

internal sealed class WeatherForecastEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
	public void Configure(EntityTypeBuilder<UserEntity> builder)
	{
		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.ValueGeneratedNever();
	}
}
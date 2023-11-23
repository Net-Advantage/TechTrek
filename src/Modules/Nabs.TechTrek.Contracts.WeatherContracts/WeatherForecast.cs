namespace Nabs.TechTrek.Contracts.WeatherContracts;

public class WeatherForecast
{
	public int Id => GenerateId();
	public DateOnly Date { get; set; }
	public double TemperatureC { get; set; }
	public double TemperatureF => 32.0D + (int)(TemperatureC / 0.5556D);
	public string? Summary { get; set; }

	private int GenerateId()
	{
		var result = (Date.Year * 10000) + (Date.Month * 100) + Date.Day;
		return result;
	}
}

using Microsoft.AspNetCore.Mvc;
using Nabs.TechTrek.Contracts.WeatherContracts;
using Nabs.TechTrek.Modules.WeatherModule;

namespace Nabs.TechTrek.WebApi.Controllers;

//TODO: DWS: This is where the Code Generator will generate the client code.

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
	[HttpGet(Name = "GetWeatherForecast")]
	public async Task<WeatherForecastResponse> Get()
	{
		return await GetWeatherForecastHandler.Handle();
	}
}

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddNabsAuthorization();
builder.Services.AddTransient<IApplicationContext, ApplicationContext>();

builder.Services.AddDaprClient();

builder.Services.AddWeatherModule();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseNabsAuthorization();

app.MapControllers();

app.MapDefaultEndpoints();

app.Run();

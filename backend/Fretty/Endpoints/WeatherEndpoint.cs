namespace Fretty.Endpoints;

public class WeatherEndpoint: IEndpoint
{
    private const string Path = "/weatherforecast";
    private static readonly string[] Summaries = [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Path, HandleWeatherRequest)
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
    
    private WeatherForecast[] HandleWeatherRequest()
    {
        var forecast =  Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
            .ToArray();
        
        return forecast;
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

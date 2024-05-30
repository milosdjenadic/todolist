using Assignment.Application.Common.Interfaces;

namespace Assignment.Infrastructure.Apis;
public class WeatherForecastApi : IWeatherForecastApi
{
    public int GetTemperature(string cityName, DateTime time)
    {
        return new Random().Next(-10, 40);
    }
}

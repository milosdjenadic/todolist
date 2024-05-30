using Assignment.Application.Common.Interfaces;
using Assignment.Application.WatherForecast.Commands.GetCities;

namespace Assignment.Application.WatherForecast.Commands.GetTemperature;
public record class GetCityTemperatureCommand(string Name, DateTime Time) : IRequest<int>;

public class GetCityTemperatureCommandHandler : IRequestHandler<GetCityTemperatureCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IWeatherForecastApi _weatherForecastApi;

    public GetCityTemperatureCommandHandler(IApplicationDbContext context, IWeatherForecastApi weatherForecastApi)
    {
        _context = context;
        _weatherForecastApi = weatherForecastApi;
    }

    public Task<int> Handle(GetCityTemperatureCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_weatherForecastApi.GetTemperature(request.Name, request.Time));
    }
}

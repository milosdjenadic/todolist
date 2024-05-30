using Assignment.Domain.Entities;

namespace Assignment.Application.WatherForecast.Commands.GetCities;
public class CityDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<City, CityDto>();
        }
    }
}

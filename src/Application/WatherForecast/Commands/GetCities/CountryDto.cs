using Assignment.Domain.Entities;

namespace Assignment.Application.WatherForecast.Commands.GetCities;
public class CountryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<CityDto> Cities { get; set; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Country, CountryDto>();
        }
    }
}

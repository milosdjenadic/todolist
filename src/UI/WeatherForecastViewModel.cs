using Assignment.Application.WatherForecast.Commands.GetCities;
using Assignment.Application.WatherForecast.Commands.GetTemperature;
using Caliburn.Micro;
using MediatR;

namespace Assignment.UI;
public class WeatherForecastViewModel : Screen
{
    private readonly ISender _sender;
    private CountryDto _selectedCountry;
    private CityDto _selectedCity;
    private BindableCollection<CountryDto> _countries;
    private DateTime? _time;
    private int? _temperature;

    public BindableCollection<CountryDto> Countries
    {
        get => _countries;
        private set
        {
            _countries = value;
            NotifyOfPropertyChange();
        }
    }

    public CountryDto SelectedCountry
    {
        get => _selectedCountry;
        set
        {
            _selectedCountry = value;
            NotifyOfPropertyChange();
        }
    }

    public CityDto SelectedCity
    {
        get => _selectedCity;
        set
        {
            _selectedCity = value;
            NotifyOfPropertyChange();
            GetTemperature();
        }
    }

    public DateTime? Time
    {
        get => _time; set
        {
            _time = value;
            NotifyOfPropertyChange();
        }
    }

    public int? Temperature
    {
        get => _temperature; set
        {
            _temperature = value;
            NotifyOfPropertyChange();
        }
    }

    public WeatherForecastViewModel(ISender sender)
    {
        _sender = sender;
        Initialize();
    }

    private async void GetTemperature()
    {
        if (SelectedCity == null)
        {
            Temperature = null;
            Time = null;
        }

        var now = DateTime.Now;
        var request = new GetCityTemperatureCommand(SelectedCity.Name, now);
        var result = await _sender.Send(request);
        Time = now;
        Temperature = result;
    }

    private async void Initialize()
    {
        var result = await _sender.Send(new GetCountriesCommand());
        Countries = new BindableCollection<CountryDto>(result);
    }
}

using System.Windows.Input;
using Assignment.UI.WindowManagement;
using Caliburn.Micro;

namespace Assignment.UI
{
    public class MainViewModel : PropertyChangedBase
    {
        private readonly IWindowManagementService _windowManagementService;

        public ICommand TodoListManagment { get; }
        public ICommand WeatherForecast { get; }

        public MainViewModel(IWindowManagementService windowManagementService)
        {
            _windowManagementService = windowManagementService;
            TodoListManagment = new RelayCommand(ShowTodoListManagment);
            WeatherForecast = new RelayCommand(ShowWeatherForecast);
        }

        private void ShowWeatherForecast(object obj)
        {
            throw new NotImplementedException();
        }

        private async void ShowTodoListManagment(object obj)
        {
            await _windowManagementService.ShowDialog<TodoManagmentViewModel>();
        }
    }
}

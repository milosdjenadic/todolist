using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.UI.WindowManagement;
public class WindowManagementService : IWindowManagementService
{
    private readonly IWindowManager _windowManager;
    private readonly IServiceProvider _serviceProvider;

    public WindowManagementService(IWindowManager windowManager, IServiceProvider serviceProvider)
    {
        _windowManager = windowManager;
        _serviceProvider = serviceProvider;
    }

    public async Task<bool?> ShowDialog<T>(params object[] parameters)
        where T : class
    {
        var viewModel = ActivatorUtilities.CreateInstance<T>(_serviceProvider, parameters);
        return await _windowManager.ShowDialogAsync(viewModel);
    }
}

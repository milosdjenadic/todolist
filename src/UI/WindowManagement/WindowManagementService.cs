using Assignment.Domain.Entities;
using Caliburn.Micro;

namespace Assignment.UI.WindowManagement;
public class WindowManagementService : IWindowManagementService
{
    private readonly IWindowManager _windowManager;

    public WindowManagementService(IWindowManager windowManager)
    {
        _windowManager = windowManager;
    }

    public async Task<bool?> ShowDialog<T>()
        where T : class
    {
        var viewModel = IoC.Get<T>();
        return await _windowManager.ShowDialogAsync(viewModel);
    }
}


namespace Assignment.UI.WindowManagement;
public interface IWindowManagementService
{
    Task<bool?> ShowDialog<T>()
        where T : class;
}

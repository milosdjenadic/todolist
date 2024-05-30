
namespace Assignment.UI.WindowManagement;
public interface IWindowManagementService
{
    Task<bool?> ShowDialog<T>(params object[] parameters) where T : class;
}

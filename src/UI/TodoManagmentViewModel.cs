using System.Windows.Input;
using Assignment.Application.TodoItems.Commands.DoneTodoItem;
using Assignment.Application.TodoLists.Queries.GetTodos;
using Assignment.UI.WindowManagement;
using Caliburn.Micro;
using MediatR;

namespace Assignment.UI;
internal class TodoManagmentViewModel : Screen
{
    private readonly ISender _sender;
    private readonly IWindowManagementService _windowManagementService;
    private IList<TodoListDto> todoLists;

    public IList<TodoListDto> TodoLists
    {
        get
        {
            return todoLists;
        }
        set
        {
            todoLists = value;
            NotifyOfPropertyChange(() => TodoLists);
        }
    }

    private TodoListDto _selectedTodoList;
    public TodoListDto SelectedTodoList
    {
        get => _selectedTodoList;
        set
        {
            _selectedTodoList = value;
            NotifyOfPropertyChange(() => SelectedTodoList);
        }
    }

    private TodoItemDto _selectedItem;
    public TodoItemDto SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            NotifyOfPropertyChange(() => SelectedItem);
        }
    }

    public ICommand AddTodoListCommand { get; private set; }
    public ICommand AddTodoItemCommand { get; private set; }
    public ICommand DoneTodoItemCommand { get; private set; }

    public TodoManagmentViewModel(ISender sender, IWindowManagementService windowManagementService)
    {
        _sender = sender;
        _windowManagementService = windowManagementService;
        Initialize();
    }

    private async void Initialize()
    {
        await RefreshTodoLists();

        AddTodoListCommand = new RelayCommand(AddTodoList);
        AddTodoItemCommand = new RelayCommand(AddTodoItem, p => SelectedTodoList != null);
        DoneTodoItemCommand = new RelayCommand(DoneTodoItem, p => SelectedItem != null && !SelectedItem.Done);
    }

    private async Task RefreshTodoLists()
    {
        var selectedListId = SelectedTodoList?.Id;

        TodoLists = await _sender.Send(new GetTodosQuery());

        if (selectedListId.HasValue && selectedListId.Value > 0)
        {
            SelectedTodoList = TodoLists.FirstOrDefault(list => list.Id == selectedListId.Value);
        }
    }

    private async void AddTodoList(object obj)
    {
        var result = await _windowManagementService.ShowDialog<TodoListViewModel>();
        if (result ?? false)
        {
            await RefreshTodoLists();
        }
    }

    private async void AddTodoItem(object obj)
    {
        var result = await _windowManagementService.ShowDialog<TodoItemViewModel>(SelectedTodoList.Id);
        if (result ?? false)
        {
            await RefreshTodoLists();
        }
    }

    private async void DoneTodoItem(object obj)
    {
        await _sender.Send(new DoneTodoItemCommand(SelectedItem.Id));
        await RefreshTodoLists();
    }
}

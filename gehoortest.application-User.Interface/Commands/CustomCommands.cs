using System;
using System.Windows.Input;

namespace gehoortest.application_User.Interface.Commands;

public class CustomCommands : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly Action<object> _executeAction;

    public bool CanExecute(object? parameter)
    {
        return true; // not implemented
    }

    public void Execute(object parameter)
    {
        _executeAction?.Invoke(parameter);
    }

    public CustomCommands(Action<object> executeAction)
    {
        _executeAction = executeAction;
    }
}

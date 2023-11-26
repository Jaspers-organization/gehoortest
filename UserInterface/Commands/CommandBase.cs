using System;
using System.Windows.Input;

namespace UserInterface.Commands;

public abstract class CommandBase<T> : ICommand
{
    public event EventHandler? CanExecuteChanged;

    protected Action<T>? command;

    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is T typedParameter && command != null)
        {
            command.Invoke(typedParameter);
        }
    }
}

using System;
using System.Windows.Input;

namespace UserInterface.Commands;

internal abstract class CommandBase : ICommand
{
    protected Action? command;

    public event EventHandler? CanExecuteChanged;

    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        command?.Invoke();
    }
}

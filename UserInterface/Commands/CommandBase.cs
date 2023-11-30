using BusinessLogic.IModels;
using System;
using System.Windows.Input;

namespace UserInterface.Commands;

public abstract class CommandBase : ICommand
{
    public event EventHandler? CanExecuteChanged;
    protected Action? command;
    protected Action<int>? intCommand;
    protected Action<string>? stringCommand;
    protected Action<ITest>? itestCommand;

    public virtual bool CanExecute(object? parameter)  => true;

    public void Execute(object? parameter)
    {
        if (command != null)
        {
            command.Invoke();
            return;
        }
        if (intCommand != null)
        {
            intCommand.Invoke((int)parameter!);
            return;
        }
        if (stringCommand != null)
        {
            stringCommand.Invoke((string)parameter!);
            return;
        }
        if (itestCommand != null)
        {
            itestCommand.Invoke((ITest)parameter!);
            return;
        }
    }
}

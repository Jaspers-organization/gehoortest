using BusinessLogic.Models;
using System;
using System.Windows.Input;

namespace UserInterface.Commands;

public abstract class CommandBase : ICommand
{
    public event EventHandler? CanExecuteChanged;
    protected Action? command;
    protected Action<int>? intCommand;
    protected Action<string>? stringCommand;
    protected Action<Guid>? guidCommand;
    protected Action<Test>? testCommand;
    protected Action<TargetAudience>? targetAudienceCommand;

    public virtual bool CanExecute(object? parameter) => true;

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
        if (guidCommand != null)
        {
            guidCommand.Invoke((Guid)parameter!);
            return;
        }
        if (testCommand != null)
        {
            testCommand.Invoke((Test)parameter!);
            return;
        }
        if (targetAudienceCommand != null)
        {
            targetAudienceCommand.Invoke((TargetAudience)parameter!);
            return;
        }
    }
}

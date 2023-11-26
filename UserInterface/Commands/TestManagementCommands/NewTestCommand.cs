using System;

namespace UserInterface.Commands.TestManagementCommands;

public class NewTestCommand: CommandBase<object>
{
    public NewTestCommand(Action<object> command)
    {
        this.command = command;
    }
}

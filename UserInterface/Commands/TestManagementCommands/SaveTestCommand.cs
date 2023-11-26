using BusinessLogic.IModels;
using System;

namespace UserInterface.Commands.TestManagementCommands;

public class SaveTestCommand: CommandBase<ITest>
{
    public SaveTestCommand(Action<ITest> command)
    {
        this.command = command;
    }
}

using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands.TestManagementCommands;

internal class ITestCommand : CommandBase<ITest>
{
    public ITestCommand(Action<ITest> command)
    {
        this.command = command;
    }
}
using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands.TestManagementCommands;

public class DeleteTestCommand : CommandBase<ITest>
{
    public DeleteTestCommand(Action<ITest> command)
    {
        this.command = command;
    }
}
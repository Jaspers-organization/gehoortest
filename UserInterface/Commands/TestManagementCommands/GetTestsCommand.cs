using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands.TestManagementCommands;

public class GetTestsCommand : CommandBase<int>
{
    public GetTestsCommand(Action<int> command)
    {
        this.command = command;
    }
}


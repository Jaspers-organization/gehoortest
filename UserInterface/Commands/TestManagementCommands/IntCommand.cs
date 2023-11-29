using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands.TestManagementCommands;

internal class IntCommand : CommandBase<int>
{
    public IntCommand(Action<int> command)
    {
        this.command = command;
    }
}
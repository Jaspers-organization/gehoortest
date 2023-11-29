using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands.TestManagementCommands;

internal class StringCommand : CommandBase<string>
{
    public StringCommand(Action<string> command)
    {
        this.command = command;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands.TestManagementCommands;

internal class CloseModalCommand : CommandBase<object>
{
    public CloseModalCommand(Action<object> command)
    {
        this.command = command;
    }
}

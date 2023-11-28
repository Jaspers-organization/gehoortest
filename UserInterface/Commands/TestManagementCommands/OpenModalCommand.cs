using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands.TestManagementCommands;

class OpenModalCommand: CommandBase<object>
{
    public OpenModalCommand(Action<object> command)
    {
        this.command = command;
    }
}

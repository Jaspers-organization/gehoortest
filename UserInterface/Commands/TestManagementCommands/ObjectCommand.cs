using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands.TestManagementCommands;

internal class ObjectCommand : CommandBase<object>
{
    public ObjectCommand(Action<object> command)
    {
        this.command = command;
    }
}

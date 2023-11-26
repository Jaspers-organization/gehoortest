using System;

namespace UserInterface.Commands;

internal class Command : CommandBase
{
    public Command(Action commmand)
    {
        this.command = commmand;
    }
}

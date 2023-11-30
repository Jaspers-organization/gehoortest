using System;

namespace UserInterface.Commands;

internal class Command : CommandBase
{
    public Command(Action command) => this.command = command;
    public Command(Action<int> command) => this.intCommand = command;
    public Command(Action<string> command) => this.stringCommand = command;


   
}

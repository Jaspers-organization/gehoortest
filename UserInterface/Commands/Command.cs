using BusinessLogic.IModels;
using BusinessLogic.Models;
using System;

namespace UserInterface.Commands;

internal class Command : CommandBase
{
    public Command(Action command) => this.command = command;
    public Command(Action<int> command) => intCommand = command;
    public Command(Action<string> command) => stringCommand = command;
    public Command(Action<Test> command) => testCommand = command;
    public Command(Action<Guid> command) => guidCommand = command;

}
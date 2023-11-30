using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Commands;

internal class Command : CommandBase
{
        public Command(Action command) => this.command = command;
        public Command(Action<int> command) => this.intCommand = command;
        public Command(Action<string> command) => this.stringCommand = command;
        public Command(Action<ITest> command) => this.itestCommand = command;
    
}

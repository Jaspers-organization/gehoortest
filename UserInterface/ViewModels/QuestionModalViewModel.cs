using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserInterface.Commands.TestManagementCommands;
using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class QuestionModalViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;

    public ICommand CloseModalCommand => new CloseModalCommand(CloseModal);

    public QuestionModalViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
    }

    private void CloseModal(object obj)
    {
        navigationStore.CloseModal();
    }
}

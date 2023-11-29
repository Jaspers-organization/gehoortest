using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserInterface.Commands.TestManagementCommands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class ConfirmationModalViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private string _text;
    public string Text
    {
        get { return _text; }
        set { _text = value; OnPropertyChanged(nameof(Text)); }
    }

    public ICommand CloseModalCommand => new CloseModalCommand(CloseModal);
    public ConfirmationModalViewModel(NavigationStore navigationStore, string text)
    {
        this.navigationStore = navigationStore;
        _text = text;
        Text = _text;
    }

    private void CloseModal(object obj)
    {
        navigationStore.CloseModal();
    }
}

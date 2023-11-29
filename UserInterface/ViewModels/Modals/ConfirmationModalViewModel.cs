using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserInterface.Commands;
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

    public ICommand CloseModalCommand => new Command(CloseModal);
    public ConfirmationModalViewModel(NavigationStore navigationStore, string text)
    {
        this.navigationStore = navigationStore;
        Text = text;
    }

    private void CloseModal()
    {
        navigationStore.CloseModal();
    }
}

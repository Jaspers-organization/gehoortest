using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class ErrorModalViewModal : ViewModelBase
{
    private readonly NavigationStore navigationStore;

    private string _text;
    public string Text
    {
        get { return _text; }
        set { _text = value; OnPropertyChanged(nameof(Text)); }
    }

    public ICommand ConfirmCommand => new Command(Confirm);

    public ErrorModalViewModal(NavigationStore navigationStore, string text)
    {
        this.navigationStore = navigationStore;
        Text = text;
    }
    private void Confirm()
    {
        CloseModal();
    }
    private void CloseModal()
    {
        navigationStore.CloseModal();
    }
}

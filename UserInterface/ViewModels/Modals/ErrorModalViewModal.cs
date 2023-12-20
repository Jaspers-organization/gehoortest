using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class ErrorModalViewModal : ViewModelBase
{
    #region Dependencies
    private readonly NavigationStore navigationStore;
    #endregion

    #region Properties
    private string _text;
    public string Text
    {
        get { return _text; }
        set { _text = value; OnPropertyChanged(nameof(Text)); }
    }
    #endregion

    #region Commands
    public ICommand ConfirmCommand => new Command(Confirm);
    public ICommand DenyCommand => new Command(Deny);
    #endregion

    public ErrorModalViewModal(NavigationStore navigationStore, string text)
    {
        this.navigationStore = navigationStore;
        Text = text;
    }
    private void Confirm()
    {
        CloseModal();
    }
    private void Deny()
    {
        CloseModal();
    }   
    private void CloseModal()
    {
        navigationStore.CloseModal();
    }
}

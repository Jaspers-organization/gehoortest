using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using System;

namespace UserInterface.ViewModels.Modals;

internal class ErrorModalViewModal : ViewModelBase
{
    #region Dependencies
    private readonly NavigationStore navigationStore;
    private readonly Action action;
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
    private void PerformAction()
    {
        action?.Invoke();
    }
    private void CloseModal()
    {
        navigationStore.CloseModal();
        PerformAction();
    }
}

using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using BusinessLogic.Interfaces;
using System;

namespace UserInterface.ViewModels.Modals;

internal class ConfirmationModalViewModel : ViewModelBase
{
    #region Dependencies
    private readonly NavigationStore _navigationStore;
    private readonly Action _action;
    #endregion

    #region Propertys
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

    private IConfirmation _confirmation;

    public ConfirmationModalViewModel(NavigationStore navigationStore, string text , IConfirmation confirmation, Action action)
    {
        _navigationStore = navigationStore;
        _confirmation = confirmation;
        Text = text;
        _action = action;
    }
    public void Confirm()
    {
        _confirmation.SetConfirmed(true);
        CloseModal();
    }
    public void Deny() {
        _confirmation.SetConfirmed(false);
        CloseModal();
    }
    public void PerformAction()
    {
        _action?.Invoke(); 
    }
    private void CloseModal()
    {
        _navigationStore.CloseModal();
        PerformAction();
    }
}

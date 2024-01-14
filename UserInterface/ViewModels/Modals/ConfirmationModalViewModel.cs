using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using BusinessLogic.Interfaces;
using System;

namespace UserInterface.ViewModels.Modals;

internal class ConfirmationModalViewModel : ViewModelBase
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

    private IConfirmation confirmation;

    public ConfirmationModalViewModel(NavigationStore navigationStore, string text , IConfirmation confirmation, Action action)
    {
        this.navigationStore = navigationStore;
        this.confirmation = confirmation;
        Text = text;
        this.action = action;
    }
    private void Confirm()
    {
        confirmation.IsConfirmed = true;
        CloseModal();
    }
    private void Deny() {
        confirmation.IsConfirmed = false;
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

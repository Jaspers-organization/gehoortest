using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using System;

namespace UserInterface.ViewModels.Modals;

internal class ErrorModalViewModal : ViewModelBase
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

    public ErrorModalViewModal(NavigationStore navigationStore, string text)
    {
        _navigationStore = navigationStore;
        Text = text;
    }
    public void Confirm()
    {
        CloseModal();
    }
    public void Deny()
    {
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

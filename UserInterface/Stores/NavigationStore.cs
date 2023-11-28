using UserInterface.ViewModels;
using System;

namespace UserInterface.Stores;

internal class NavigationStore
{
    #region ViewModel
    private ViewModelBase? _currentViewModel;

    public event Action? CurrentViewModelChanged;

    public ViewModelBase? CurrentViewModel
    {
        get { return _currentViewModel; }
        set { _currentViewModel = value; OnCurrentViewModelChanged(); }
    }
    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
    #endregion

    #region ModalViewModel
    public event Action? CurrentModalViewModelChanged;

    private ViewModelBase? _currentModalViewModel;
    public ViewModelBase? CurrentModalViewModel
    {
        get { return _currentModalViewModel; }
        set { _currentModalViewModel = value; OnCurrentModalViewModelChanged(); }
    }

    private bool IsOpen = false;

    public bool returnModalState()
    {
        return IsOpen;
    }
    public void OpenModal()
    {
        IsOpen = true;
    }

    public void CloseModal()
    {
        CurrentModalViewModel = null;
        IsOpen = false;
    }
    private void OnCurrentModalViewModelChanged()
    {
        CurrentModalViewModelChanged?.Invoke();
    }
    #endregion

}

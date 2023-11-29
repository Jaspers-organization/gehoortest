using UserInterface.ViewModels;
using System;

namespace UserInterface.Stores;

internal class NavigationStore
{
    public event Action? CurrentViewModelChanged;
    public event Action? CurrentModalViewModelChanged;
    public event Action? IsModelOpenChanged;

    private ViewModelBase? _currentViewModel;
    private ViewModelBase? _currentModalViewModel;
    private bool _isModelOpen = false;

    public ViewModelBase? CurrentViewModel
    {
        get { return _currentViewModel; }
        set { _currentViewModel = value; OnCurrentViewModelChanged(); }
    }
    public ViewModelBase? CurrentModalViewModel
    {
        get { return _currentModalViewModel; }
        set { _currentModalViewModel = value; OnCurrentModalViewModelChanged(); }
    }
    public bool IsModelOpen
    {
        get { return _isModelOpen; }
        set { _isModelOpen = value; OnIsModelOpenChanged(); }
    }

    public void OpenModal(ViewModelBase viewModal)
    {
        CurrentModalViewModel = viewModal;
        IsModelOpen = true;
    }

    public void CloseModal()
    {
        CurrentModalViewModel = null;
        IsModelOpen = false;
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

    private void OnCurrentModalViewModelChanged()
    {
        CurrentModalViewModelChanged?.Invoke();
    }

    private void OnIsModelOpenChanged()
    {
        IsModelOpenChanged?.Invoke();
    }

}

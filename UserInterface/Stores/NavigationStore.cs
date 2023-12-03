using System;
using UserInterface.ViewModels;

namespace UserInterface.Stores;

internal class NavigationStore
{
    public event Action? CurrentViewModelChanged;
    public event Action? IsModalOpenChanged;

    private ViewModelBase? _currentViewModel;
    private ViewModelBase? _currentModalViewModel;
    private bool _isModalOpen = false;

    public ViewModelBase? CurrentViewModel
    {
        get { return _currentViewModel; }
        set { _currentViewModel = value; OnCurrentViewModelChanged(); }
    }
    public ViewModelBase? CurrentModalViewModel
    {
        get { return _currentModalViewModel; }
        set { _currentModalViewModel = value; }
    }
    public bool IsModalOpen
    {
        get { return _isModalOpen; }
        set { _isModalOpen = value; OnIsModalOpenChanged(); }
    }

    public void OpenModal(ViewModelBase viewModal)
    {
        CurrentModalViewModel = viewModal;
        IsModalOpen = true;
    }

    public void CloseModal()
    {
        CurrentModalViewModel = null;
        IsModalOpen = false;
    }

    public void CloseModal(ViewModelBase viewModal)
    {
        CurrentModalViewModel = null;
        CurrentViewModel = viewModal;
        IsModalOpen = false;
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

    private void OnIsModalOpenChanged()
    {
        IsModalOpenChanged?.Invoke();
    }
}
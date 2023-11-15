using gehoortest.application_User.Interface.ViewModels;
using System;

namespace gehoortest.application_User.Interface.Stores;

internal class NavigationStore
{
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
}

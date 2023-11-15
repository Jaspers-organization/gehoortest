using gehoortest.application_User.Interface.Stores;

namespace gehoortest.application_User.Interface.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{

    private readonly NavigationStore? _navigationStore;

    public ViewModelBase? CurrentViewModel => _navigationStore?.CurrentViewModel;

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}

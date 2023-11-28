using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{

    private readonly NavigationStore? _navigationStore;

    public ViewModelBase? CurrentViewModel => _navigationStore?.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _navigationStore?.CurrentModalViewModel;

    //public bool IsModalOpen => _navigationStore.returnModalState();

    private bool _isModalOpen;
    public bool IsModalOpen
    {
        get { return _isModalOpen; }
        set { _isModalOpen = value; OnPropertyChanged(nameof(IsModalOpen)); }
    }


    public MainWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        _navigationStore.CurrentModalViewModelChanged += OnCurrentModalViewModelChanged;
        IsModalOpen = _navigationStore.returnModalState();
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
    private void OnCurrentModalViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOpen));

    }
}

using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    private NavigationStore navigationStore;

    public ViewModelBase? CurrentViewModel => navigationStore?.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => navigationStore?.CurrentModalViewModel;

    private string _showModal = "Hidden";
    public string ShowModal
    {
        get { return _showModal; }
        set { _showModal = value; OnPropertyChanged(nameof(ShowModal)); }
    }

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;

        this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        this.navigationStore.CurrentModalViewModelChanged += OnCurrentModalViewModelChanged;
        this.navigationStore.IsModelOpenChanged += OnIsModelOpenChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void OnCurrentModalViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
    }

    private void OnIsModelOpenChanged()
    {
        ShowModal = navigationStore.IsModelOpen ? "Visible" : "Hidden";
    }
}
